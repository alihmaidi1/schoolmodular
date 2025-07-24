using Shared.Domain.MediatR;
using Shared.Domain.OperationResult;

namespace Identity.Application.Auth.Admin.Command.Login;


public class LoginAdminRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    
}

public sealed class LoginAdminCommand:LoginAdminRequest, ICommand<TResult<LoginAdminResponse>>
{
}