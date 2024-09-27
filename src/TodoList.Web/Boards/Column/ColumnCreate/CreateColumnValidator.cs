using FastEndpoints;
using FluentValidation;
using TodoList.Infrastructure.Data.Config;

namespace TodoList.Web.Boards.Column;

public class CreateColumnValidator : Validator<CreateColumnRequest>
{
    public CreateColumnValidator()
    {
        RuleFor(x => x.BoardId)
            .NotEmpty().WithMessage("Board ID is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Column name is required.")
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
    }
}
