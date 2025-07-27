using Identity.Domain.Repository;
using Identity.Domain.Services.Hash;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Extensions;
using Shared.Domain.MediatR;
using Shared.Domain.OperationResult;
using Shared.Domain.Services.Twilio;


namespace Identity.Application.Auth.Admin.Command.ForgetPassword;

internal sealed class ForgetPasswordCommandHandler: ICommandHandler<ForgetPasswordCommand,TResult<bool>>
{

    private readonly IUnitOfWork  _unitOfWork;
    private readonly IWordHasherService _wordHasherService;


    public ForgetPasswordCommandHandler(IWordHasherService wordHasherService,IUnitOfWork  unitOfWork,ISmsTwilioService  smsTwilioService)
    {
        _unitOfWork=unitOfWork;
        _wordHasherService=wordHasherService;
    }
    public async Task<TResult<bool>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var admin=await _unitOfWork._adminRepository.GetQueryable()
            .FirstOrDefaultAsync(x=>x.Email==request.Email,cancellationToken);
        if (admin == null)
        {
            
            return Result.ValidationFailure<bool>(Error.ValidationFailures("Email or Password is not valid."));
        }
        var result=admin.SetForgetCode(string.Empty.GenerateRandomString(5),5,_wordHasherService);
        if (result.isFailure)
        {
            return result;
        }
        await _unitOfWork._adminRepository.UpdateAsync(admin);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(true);
    }
}