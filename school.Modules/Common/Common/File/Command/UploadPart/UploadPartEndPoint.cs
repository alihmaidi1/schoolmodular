// using Carter;
// using Mapster;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Routing;
// using Shared.Domain.CQRS;
// using Shared.Domain.OperationResult;
//
// namespace Common.File.Command.UploadPart;
//
// public class UploadPartEndPoint: ICarterModule
// {
//     public void AddRoutes(IEndpointRouteBuilder app)
//     {
//         
//         app.MapPost("/files/uploadPart",
//                 async ([FromForm]UploadPartRequest request,[FromHeader]Guid RequestId,ICommandHandler<UploadPartCommand> handler,CancellationToken cancellationToken) =>
//                 {
//
//                     UploadPartCommand command =request.Adapt<UploadPartCommand>();
//                     command.RequestId = RequestId;
//                     var result=await handler.Handle(command, cancellationToken);
//                     return  result;
//                 })
//             .Produces<TResult<string>>(StatusCodes.Status201Created)
//             .ProducesProblem(StatusCodes.Status400BadRequest)
//             .DisableAntiforgery()
//             .WithSummary("Upload part")
//             .WithTags("Files")
//             
//             .WithDescription("Upload part");
//     }
// }