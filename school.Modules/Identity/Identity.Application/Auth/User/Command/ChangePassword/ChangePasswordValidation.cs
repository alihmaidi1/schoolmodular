// using FluentValidation;
//
// namespace Identity.Application.Auth.User.Command.ChangePassword;
//
// internal sealed class ChangePasswordValidation: AbstractValidator<ChangePasswordRequest>
// {
//
//     public ChangePasswordValidation()
//     {
//         
//         RuleFor(x => x.Password)
//             .NotNull()
//             .MinimumLength(8)
//             .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
//             .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
//             .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
//
//         
//     }
//     
// }