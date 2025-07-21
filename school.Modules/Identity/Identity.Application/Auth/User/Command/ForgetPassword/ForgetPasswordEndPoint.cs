using Carter;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Domain.CQRS;
using Shared.Domain.OperationResult;

namespace Identity.Application.Auth.User.Command.ForgetPassword;

public class ForgetPasswordEndPoint: ICarterModule
{
    
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        
        app.MapPost("/users/forgetPassword", 
                async ([FromBody]  ForgetPasswordRequest request,[FromHeader]Guid RequestId,IDispatcher  dispatcher,CancellationToken cancellationToken) =>
                {
                    ForgetPasswordCommand command = request.Adapt<ForgetPasswordCommand>();
                    command.RequestId = RequestId;
                    var result=await dispatcher.Send(command, cancellationToken);
                    return result.ToActionResult();
                })
            .Produces<TResult<bool>>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithTags("UserAuthentication")
            .WithSummary("user forget password")
            .WithDescription("user forget password");
    }
}