// using FluentValidation;
//
// namespace Identity.Application.Auth.User.Command.Login;
//
// internal sealed class LoginUserValidation: AbstractValidator<LoginUserCommand>
// {
//
//     public LoginUserValidation()
//     {
//
//         RuleFor(x => x.Password)
//             .NotNull()
//             .MinimumLength(8)
//             .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
//             .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
//             .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
//
//         RuleFor(x => x.Email)
//             .NotNull()
//             .EmailAddress();
//     }
//     
// }