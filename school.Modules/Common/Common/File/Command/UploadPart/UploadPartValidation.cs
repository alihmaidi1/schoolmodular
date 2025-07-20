// using FluentValidation;
//
// namespace Common.File.Command.UploadPart;
//
// internal sealed class UploadPartValidation: AbstractValidator<UploadPartRequest>
// {
//
//     public UploadPartValidation()
//     {
//         
//         RuleFor(x => x.fileName)
//             .NotEmpty()
//             .MustAsync(async (video, cancellationToken) =>
//             {
//                 var extension = Path.GetExtension(video);
//                 return !string.IsNullOrEmpty(extension) &&
//                        extension.Equals(".mp4", StringComparison.OrdinalIgnoreCase);
//             })
//             .WithMessage("Invalid video format. Only .mp4 formats are allowed.");
//
//         RuleFor(x => x.partNumber)
//             .GreaterThanOrEqualTo(1);
//         RuleFor(x => x.uploadId)
//             .NotEmpty();
//
//     }
//     
// }