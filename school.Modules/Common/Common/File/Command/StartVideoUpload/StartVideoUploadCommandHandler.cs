// using Common.Services.File;
// using Microsoft.AspNetCore.Http;
// using Shared.Domain.CQRS;
// using Shared.Domain.OperationResult;
//
//
// namespace Common.File.Command.StartVideoUpload;
//
// internal sealed class StartVideoUploadCommandHandler: ICommandHandler<StartVideoUploadCommand>
// {
//
//     private readonly IAwsStorageService  _awsStorageService;
//     public StartVideoUploadCommandHandler(IAwsStorageService  awsStorageService)
//     {
//         _awsStorageService= awsStorageService;
//         
//     }
//     public async Task<IResult> Handle(StartVideoUploadCommand request, CancellationToken cancellationToken)
//     {
//         var uploadResult=await _awsStorageService.InitiateChunkedVideoUpload(request.Video);
//         return Result.Success(uploadResult.Value).ToActionResult();
//         
//     }
// }