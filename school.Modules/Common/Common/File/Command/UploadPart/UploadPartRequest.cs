// using Microsoft.AspNetCore.Http;
// using Shared.Domain.CQRS;
//
// namespace Common.File.Command.UploadPart;
//
// public class UploadPartRequest
// {
//     public string uploadId { get; set; }
//     
//     public string fileName { get; set; }
//
//     public int partNumber { get; set; }
//     
//
//     public IFormFile file { get; set; }
//
// }
//
// public sealed class UploadPartCommand:UploadPartRequest, ICommand
// {
//     public Guid? RequestId { get; set; }
// }