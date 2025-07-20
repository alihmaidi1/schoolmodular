// using Carter;
// using Common.File.Command.StartVideoUpload;
// using Mapster;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Routing;
// using Shared.Domain.CQRS;
// using Shared.Domain.OperationResult;
//
// namespace Common.File.Command.CompleteMultipartUpload;
//
// public class CompleteMultipartUploadEndPoint: ICarterModule
// {
//     public void AddRoutes(IEndpointRouteBuilder app)
//     {
//         
//         app.MapPost("/files/CompleteMultipartUpload",
//                 async ([FromBody]  CompleteMultipartUploadRequest request,[FromHeader]Guid RequestId,ICommandHandler<CompleteMultipartUploadCommand> handler,CancellationToken cancellationToken) =>
//                 {
//
//                     var command = request.Adapt<CompleteMultipartUploadCommand>();
//                     command.RequestId = RequestId;
//                     var result=await handler.Handle(command, cancellationToken);
//                     return  result;
//                 })
//             .Produces<TResult<bool>>(StatusCodes.Status201Created)
//             .ProducesProblem(StatusCodes.Status400BadRequest)
//             .WithSummary("completedMultipartUpload")
//             .WithTags("Files")
//             
//             .WithDescription("completedMultipartUpload");
//     }
// }