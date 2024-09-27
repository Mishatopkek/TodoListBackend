using FastEndpoints;
using FluentValidation;
using TodoList.Infrastructure.Data.Config;
using TodoList.Web.Users.SignUp;

namespace TodoList.Web.Boards.Create;

public class CreateBoardValidator : Validator<CreateBoardRequest>
{
    public CreateBoardValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.Name)
            .MinimumLength(2)
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("Board name can only contain letters, numbers, and underscores.")
            .Must(SignUpUserValidator.NotContainRestrictedWords).WithMessage("Board contains restricted words.");
    }
}
