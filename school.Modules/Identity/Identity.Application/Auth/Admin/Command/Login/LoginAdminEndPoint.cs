using Carter;
using Identity.Application.Auth.Admin.Command.Login;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Domain.CQRS;
using Shared.Domain.OperationResult;

namespace Identity.Application.Auth.Admin.Command.Login;


public class LoginAdminEndPoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/admin/login", 
                async ([FromBody]  LoginAdminRequest request,[FromHeader]Guid RequestId,IDispatcher  dispatcher,CancellationToken cancellationToken) =>
                {
                    LoginAdminCommand command = request.Adapt<LoginAdminCommand>();
                    command.RequestId = RequestId;
                    var result=await dispatcher.Send(command, cancellationToken);
                    return result;
                })
            .Produces<TResult<LoginAdminResponse>>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithTags("AdminAuthentication")
            .WithSummary("login admin to website")
            .WithDescription("login admin to website");
    }
}