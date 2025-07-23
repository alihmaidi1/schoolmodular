using Identity.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.Domain.CQRS;
using Shared.Domain.Extensions;
using Shared.Domain.OperationResult;
using Shared.Domain.Services.Twilio;


namespace Identity.Application.Auth.User.Command.ForgetPassword;

internal sealed class ForgetPasswordCommandHandler: ICommandHandler<ForgetPasswordCommand,TResult<bool>>
{

    private readonly IUnitOfWork  _unitOfWork;
    private readonly UserManager<Domain.Security.User>  _userManager;


    public ForgetPasswordCommandHandler(IUnitOfWork  unitOfWork,UserManager<Domain.Security.User>  userManager,ISmsTwilioService  smsTwilioService)
    {
        _userManager = userManager;
        _unitOfWork=unitOfWork;
    }
    public async Task<TResult<bool>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user=await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            
            return Result.ValidationFailure<bool>(Error.ValidationFailures("Email or Password is not valid."));
        }
        var result=user.SetForgetCode(string.Empty.GenerateRandomString(5),5);
        if (result.isFailure)
        {
            return result;
        }
        var _identityResult = await _userManager.UpdateAsync(user);
        if (!_identityResult.Succeeded)
        {
            return Result.InternalError<bool>(Error.Internal(_identityResult.Errors.First().Description));
            
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(true);
    }
}