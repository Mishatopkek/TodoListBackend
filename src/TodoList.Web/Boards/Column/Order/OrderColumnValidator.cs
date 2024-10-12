using FastEndpoints;
using FluentValidation;

namespace TodoList.Web.Boards.Column.Order;

public class OrderColumnValidator : Validator<OrderColumnRequest>
{
    public OrderColumnValidator()
    {
        RuleFor(x => x.ColumnId)
            .NotEmpty().WithMessage("Board ID is required.");

        RuleFor(x => x.Position);
    }
}
