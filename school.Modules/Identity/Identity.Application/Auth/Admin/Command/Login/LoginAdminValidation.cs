using FluentValidation;

namespace Identity.Application.Auth.Admin.Command.Login;

internal sealed class LoginAdminValidation: AbstractValidator<LoginAdminCommand>
{

    public LoginAdminValidation()
    {

        RuleFor(x => x.Password)
            .NotNull()
            .MinimumLength(8);
        RuleFor(x => x.Email)
            .NotNull()
            .EmailAddress();
    }
    
}