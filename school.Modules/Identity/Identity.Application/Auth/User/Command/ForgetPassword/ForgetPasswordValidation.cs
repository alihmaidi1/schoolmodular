// using FluentValidation;
//
// namespace Identity.Application.Auth.User.Command.ForgetPassword;
//
// internal sealed class ForgetPasswordValidation: AbstractValidator<ForgetPasswordRequest>
// {
//
//     public ForgetPasswordValidation()
//     {
//         
//         RuleFor(x=>x.Email)
//             .NotEmpty()
//             .EmailAddress();
//     }
//     
// }