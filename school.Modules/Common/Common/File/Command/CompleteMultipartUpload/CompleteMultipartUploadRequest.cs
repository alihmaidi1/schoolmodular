// using Common.Services.File;
// using Shared.Domain.CQRS;
//
// namespace Common.File.Command.CompleteMultipartUpload;
//
// public class CompleteMultipartUploadRequest
// {
//     public string uploadId { get; set; }
//     public string fileName { get; set; }
//     public List<PartETag> partETags { get; set; }
//
// }
//
// public sealed class CompleteMultipartUploadCommand: CompleteMultipartUploadRequest,ICommand
// {
//     public Guid? RequestId { get; set; }
// }