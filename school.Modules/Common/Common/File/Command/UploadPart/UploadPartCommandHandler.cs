// using Common.Services.File;
// using Microsoft.AspNetCore.Http;
// using Shared.Domain.CQRS;
// using Shared.Domain.OperationResult;
//
//
// namespace Common.File.Command.UploadPart;
//
// internal sealed class UploadPartCommandHandler: ICommandHandler<UploadPartCommand>
// {
//
//     
//     private readonly IAwsStorageService  _awsStorageService;
//     
//     public UploadPartCommandHandler(IAwsStorageService  awsStorageService)
//     {
//         
//         _awsStorageService= awsStorageService;
//     }
//     public async Task<IResult> Handle(UploadPartCommand request, CancellationToken cancellationToken)
//     {
//         using var stream = request.file.OpenReadStream();
//
//         var uploadResult =await _awsStorageService.UploadPartAsync(request.uploadId,
//             request.fileName,
//             request.partNumber,
//             stream);
//         return Result.Success(uploadResult).ToActionResult();
//     }
// }