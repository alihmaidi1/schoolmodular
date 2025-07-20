// using FluentValidation;
//
// namespace Common.File.Command.CompleteMultipartUpload;
//
// internal sealed class CompleteMultipartUploadValidation: AbstractValidator<CompleteMultipartUploadRequest>
// {
//
//     public CompleteMultipartUploadValidation()
//     {
//
//         RuleFor(x => x.fileName)
//             .NotEmpty();
//
//         RuleFor(x => x.uploadId)
//             .NotEmpty();
//
//         RuleFor(x => x.partETags)
//             .NotEmpty()
//             .NotNull();
//
//
//     }
//     
// }