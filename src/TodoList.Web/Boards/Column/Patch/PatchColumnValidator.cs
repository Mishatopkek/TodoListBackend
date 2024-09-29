using FastEndpoints;
using FluentValidation;
using TodoList.Infrastructure.Data.Config;

namespace TodoList.Web.Boards.Column.Patch;

public class PatchColumnValidator : Validator<PatchColumnRequest>
{
    public PatchColumnValidator()
    {
        RuleFor(x => x.ColumnId)
            .NotEmpty().WithMessage("Board ID is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Column name is required.")
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
    }
}
