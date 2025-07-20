// using Carter;
// using Mapster;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Routing;
// using Shared.Domain.CQRS;
// using Shared.Domain.OperationResult;
// namespace Common.File.Command.UploadImage;
//
// public class UploadImageEndPoint: ICarterModule
// {
//     public void AddRoutes(IEndpointRouteBuilder app)
//     {
//         app.MapPost("/images/uploadPresignedUrl",
//                 async ([FromBody]  UploadImageRequest request,[FromHeader]Guid RequestId,ICommandHandler<UploadImageCommand> handler,CancellationToken cancellationToken) =>
//                 {
//                     var command = request.Adapt<UploadImageCommand>(); 
//                     command.RequestId = RequestId;
//                     var result=await handler.Handle(command, cancellationToken);
//                     return  result;
//                 })
//             .Produces<TResult<string>>(StatusCodes.Status201Created)
//             .ProducesProblem(StatusCodes.Status400BadRequest)
//             .WithSummary("get PresignedUrl")
//             .WithTags("Files")
//             
//             .WithDescription("get PresignedUrl");
//
//     }
// }