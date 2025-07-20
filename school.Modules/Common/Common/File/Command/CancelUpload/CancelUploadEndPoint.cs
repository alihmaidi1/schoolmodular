// using Carter;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Routing;
// using Mapster;
// using Shared.Domain.CQRS;
// using Shared.Domain.OperationResult;
//
// namespace Common.File.Command.CancelUpload;
//
// public class CancelUploadEndPoint: ICarterModule
// {
//     public void AddRoutes(IEndpointRouteBuilder app)
//     {
//         
//         
//         app.MapPost("/files/CancelUpload",
//                 async ([FromBody]  CancelUploadRequest request,[FromHeader]Guid RequestId,ICommandHandler<CancelUploadCommand> handler,CancellationToken cancellationToken) =>
//                 {
//                     var command = request.Adapt<CancelUploadCommand>();
//                     command.RequestId = RequestId; 
//                     var result=await handler.Handle(command, cancellationToken);
//                     return  result;
//                 })
//             .Produces<TResult<bool>>(StatusCodes.Status201Created)
//             .ProducesProblem(StatusCodes.Status400BadRequest)
//             .WithSummary("completedMultipartUpload")
//             .WithTags("Files")
//             .WithDescription("completedMultipartUpload");
//     }
// }