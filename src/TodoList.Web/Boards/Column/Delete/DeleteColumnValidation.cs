using FastEndpoints;
using FluentValidation;

namespace TodoList.Web.Boards.Column.Delete;

public class DeleteColumnValidation : Validator<DeleteColumnRequest>
{
    public DeleteColumnValidation()
    {
        RuleFor(x => x.ColumnId)
            .NotEmpty().WithMessage("Column ID is required.");
    }
}
