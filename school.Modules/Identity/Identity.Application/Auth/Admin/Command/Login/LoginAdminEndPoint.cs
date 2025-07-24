using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Domain.OperationResult;

namespace Identity.Application.Auth.Admin.Command.Login;


public class LoginAdminEndPoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/admin/login", 
                async ([FromBody]  LoginAdminRequest request,ISender  sender,CancellationToken cancellationToken) =>
                {
                    LoginAdminCommand command = request.Adapt<LoginAdminCommand>();
                    var result=await sender.Send(command, cancellationToken);
                    return result.ToActionResult();
                })
            .Produces<TResult<LoginAdminResponse>>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithTags("AdminAuthentication")
            .WithSummary("login admin to website")
            .WithDescription("login admin to website");
    }
}