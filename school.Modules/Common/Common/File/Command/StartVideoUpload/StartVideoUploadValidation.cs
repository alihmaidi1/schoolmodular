// using FluentValidation;
//
// namespace Common.File.Command.StartVideoUpload;
//
// internal sealed class StartVideoUploadValidation: AbstractValidator<StartVideoUploadRequest>
// {
//
//     public StartVideoUploadValidation()
//     {
//         RuleFor(x => x.Video)
//             .NotEmpty()
//             .MustAsync(async (video, cancellationToken) =>
//             {
//                 var extension = Path.GetExtension(video);
//                 return !string.IsNullOrEmpty(extension) &&
//                        extension.Equals(".mp4", StringComparison.OrdinalIgnoreCase);
//             })
//             .WithMessage("Invalid video format. Only .mp4 formats are allowed.");
//
//     }
//     
// }