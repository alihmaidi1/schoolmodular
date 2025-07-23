namespace Identity.Application.Auth.Admin.Command.Login;

using Microsoft.AspNetCore.Http;
using Shared.Domain.CQRS;


public class LoginAdminRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    
}

public sealed class LoginAdminCommand:LoginAdminRequest, ICommand<IResult>
{
    public Guid? RequestId { get; set; }
}