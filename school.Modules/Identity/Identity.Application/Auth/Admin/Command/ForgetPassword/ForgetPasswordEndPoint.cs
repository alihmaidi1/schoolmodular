using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Domain.OperationResult;

namespace Identity.Application.Auth.Admin.Command.ForgetPassword;

public class ForgetPasswordEndPoint: ICarterModule
{
    
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        
        app.MapPost("/admins/forgetPassword", 
                async ([FromBody]  ForgetPasswordRequest request,CancellationToken cancellationToken) =>
                {
                    // ForgetPasswordCommand command = request.Adapt<ForgetPasswordCommand>();
                    // var result=await sender.Send(command, cancellationToken);
                    // return result.ToActionResult();
                })
            .Produces<TResult<bool>>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithTags("AdminAuthentication")
            .WithSummary("admin forget password")
            .WithDescription("admin forget password");
    }
}