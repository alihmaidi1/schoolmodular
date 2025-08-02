using Identity.Domain.Repository;
using Identity.Domain.Security;
using Identity.Domain.Services.Hash;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.MediatR;
using Shared.Domain.OperationResult;
using Shared.Domain.Repositories;

namespace Identity.Application.Auth.Admin.Command.Login;


internal sealed class LoginAdminCommandHandler: ICommandHandler<LoginAdminCommand,TResult<LoginAdminResponse>>
{
    
    private IWordHasherService  _wordHasherService;
    private readonly IIdentityUnitOfWork  _unitOfWork;
    public LoginAdminCommandHandler(IIdentityUnitOfWork unitOfWork,IWordHasherService  wordHasherService)
    {
        _unitOfWork=unitOfWork;
        _wordHasherService=wordHasherService;
    
    }
    
    public async Task<TResult<LoginAdminResponse>> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
    {

        var user = await _unitOfWork
            ._adminRepository.GetQueryable()
            .Include(x=>x.Roles)
            .FirstOrDefaultAsync(x=>x.Email==request.Email,cancellationToken);
            
            
        if (user is null||!_wordHasherService.IsValid(request.Password,user.Password))
        {
            return Result.ValidationFailure<LoginAdminResponse>(Error.ValidationFailures("Email or Password is not valid."));
            
        }
        
        var permissions = user.Roles.SelectMany(x=>x.Permissions).ToList();
        var tokenInfo = await _unitOfWork._jwtRepository.GetTokensInfo(user.Id,user.Email!,UserType.Admin,cancellationToken,permissions);
        var result = tokenInfo.Adapt<LoginAdminResponse>();
        result.permissions = permissions;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(result);

    }
}