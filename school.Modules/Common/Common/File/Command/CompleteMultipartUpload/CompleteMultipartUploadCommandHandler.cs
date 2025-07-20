// using Common.Services.File;
// using Microsoft.AspNetCore.Http;
// using Shared.Domain.CQRS;
// using Shared.Domain.OperationResult;
// using PartETag = Amazon.S3.Model.PartETag;
//
// namespace Common.File.Command.CompleteMultipartUpload;
//
// internal sealed class CompleteMultipartUploadCommandHandler: ICommandHandler<CompleteMultipartUploadCommand>
// {
//     
//     private readonly IAwsStorageService  _awsStorageService;
//
//     public CompleteMultipartUploadCommandHandler(IAwsStorageService awsStorageService)
//     {
//         
//         _awsStorageService= awsStorageService;
//     }
//     public async Task<IResult> Handle(CompleteMultipartUploadCommand request, CancellationToken cancellationToken)
//     {
//         await _awsStorageService.CompleteMultipartUploadAsync(request.uploadId, request.fileName, request.partETags.Select(x=>new PartETag()
//         {
//             ETag = x.ETag,
//             PartNumber = x.PartNumber,
//             
//         }).ToList());
//         return Result.Success(true).ToActionResult();
//     }
// }