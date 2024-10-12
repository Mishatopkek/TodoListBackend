using FastEndpoints;
using FluentValidation;

namespace TodoList.Web.Boards.Column.Card.Create;

public class CreateCardValidation : Validator<CreateCardRequest>
{
    public CreateCardValidation()
    {
        RuleFor(x => x.ColumnId)
            .NotEmpty().WithMessage("Column ID is required.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(3);
    }
}
