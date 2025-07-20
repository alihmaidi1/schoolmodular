//
// using Shared.Domain.CQRS;
//
// namespace Common.File.Command.CancelUpload;
//
// public class CancelUploadRequest
// {
//     
//     public string UploadId { get; set; }
//     
//     public string FileName { get; set; }
//     
// }
//
//
// public sealed class CancelUploadCommand:CancelUploadRequest,ICommand
// {
//     public Guid? RequestId { get; set; }
// }