//
// using FluentValidation;
//
// namespace Common.File.Command.UploadImage;
//
// internal sealed class UploadImageValidation: AbstractValidator<UploadImageRequest>
// {
//
//     public UploadImageValidation()
//     {
//         RuleFor(x => x.Image)
//             .NotEmpty()
//             .MustAsync(async (image, cancellationToken) =>
//             {
//                 var extension = Path.GetExtension(image);
//                 return !string.IsNullOrEmpty(extension) &&
//                        extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || 
//                        extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) ||
//                        extension.Equals(".png", StringComparison.OrdinalIgnoreCase);
//             })
//             .WithMessage("Invalid image format. Only .jpg, .jpeg, .png formats are allowed.");
//
//     }
//     
// }