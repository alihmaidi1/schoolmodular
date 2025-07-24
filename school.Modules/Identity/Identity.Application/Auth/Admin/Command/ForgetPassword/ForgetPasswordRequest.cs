using Shared.Domain.MediatR;
using Shared.Domain.OperationResult;

namespace Identity.Application.Auth.Admin.Command.ForgetPassword;

public class ForgetPasswordRequest
{
    
    public string Email { get; set; }
    
}


public sealed class ForgetPasswordCommand : ForgetPasswordRequest,ICommand<TResult<bool>>
{
}