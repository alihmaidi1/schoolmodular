// using Carter;
// using Mapster;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Routing;
// using Shared.Domain.CQRS;
// using Shared.Domain.OperationResult;
//
// namespace Identity.Application.Auth.User.Command.Login;
//
// public class LoginUserEndPoint: ICarterModule
// {
//     public void AddRoutes(IEndpointRouteBuilder app)
//     {
//         app.MapPost("/users/login", 
//                 async ([FromBody]  LoginUserRequest request,[FromHeader]Guid RequestId,IDispatcher  dispatcher,CancellationToken cancellationToken) =>
//                 {
//                     LoginUserCommand command = request.Adapt<LoginUserCommand>();
//                     command.RequestId = RequestId;
//                     var result=await dispatcher.Send(command, cancellationToken);
//                     return result;
//                 })
//             .Produces<TResult<LoginUserResponse>>(StatusCodes.Status201Created)
//             .ProducesProblem(StatusCodes.Status400BadRequest)
//             .WithTags("UserAuthentication")
//             .WithSummary("login user to website")
//             .WithDescription("login user to website");
//     }
// }