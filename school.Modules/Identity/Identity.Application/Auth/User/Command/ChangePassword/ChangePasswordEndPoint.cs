// using Carter;
// using Mapster;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Routing;
// using Shared.Domain.CQRS;
// using Shared.Domain.OperationResult;
//
// namespace Identity.Application.Auth.User.Command.ChangePassword;
//
// public class ChangePasswordEndPoint: ICarterModule
// {
//     public void AddRoutes(IEndpointRouteBuilder app)
//     {
//         
//         app.MapPost("/users/changePassword",
//                 async ([FromBody]  ChangePasswordRequest request,[FromHeader]Guid RequestId,ICommandHandler<ChangePasswordCommand> handler,CancellationToken cancellationToken) =>
//                 {
//                     var command = request.Adapt<ChangePasswordCommand>(); 
//                     command.RequestId=RequestId;
//                     var result=await handler.Handle(command, cancellationToken);
//                     return  result;
//                 })
//             .Produces<TResult<bool>>(StatusCodes.Status201Created)
//             .ProducesProblem(StatusCodes.Status400BadRequest)
//             // .RequireAuthorization()
//             .WithSummary("change user password")
//             .WithTags("UserAuthentication")
//             
//             .WithDescription("change user password");
//     
//     }
// }