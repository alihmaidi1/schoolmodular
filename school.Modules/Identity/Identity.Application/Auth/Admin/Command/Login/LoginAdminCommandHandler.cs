using Identity.Domain.Repository;
using Identity.Domain.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.Domain.CQRS;
using Shared.Domain.OperationResult;

namespace Identity.Application.Auth.Admin.Command.Login;


internal sealed class LoginAdminCommandHandler: ICommandHandler<LoginAdminCommand,IResult>
{
    
    private readonly UserManager<Domain.Security.User>  _userManager;
    private readonly IJwtRepository _jwtRepository;
    private readonly IUnitOfWork  _unitOfWork;
    public LoginAdminCommandHandler(IUnitOfWork  unitOfWork,UserManager<Domain.Security.User>  userManager,IJwtRepository jwtRepository)
    {
        _userManager = userManager;
        _unitOfWork=unitOfWork;
        _jwtRepository = jwtRepository;

    }
    
    public async Task<IResult> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
    {
        var user=await _userManager.FindByEmailAsync(request.Email).WaitAsync(cancellationToken);
        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password).WaitAsync(cancellationToken);
        if (user is null||!passwordValid)
        {
            return Result.ValidationFailure<LoginAdminResponse>(Error.ValidationFailures("Email or Password is not valid.")).ToActionResult();
            
        }
        
        if (!user.EmailConfirmed)
        {
            return Result.ValidationFailure<LoginAdminResponse>(Error.ValidationFailures("your email is not confirmed")).ToActionResult();
            
        }

        var result = await _jwtRepository.GetTokensInfo(user.Id,user.Email!,UserType.Admin,cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(result).ToActionResult();
    }
}