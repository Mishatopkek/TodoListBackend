using FastEndpoints;
using FluentValidation;

namespace TodoList.Web.Users.Login;

public class LoginUserValidator : Validator<LoginUserRequest>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Username or email is required")
            .MinimumLength(3);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Username or email is required")
            .MinimumLength(3);
    }
}
