using FastEndpoints;
using FluentValidation;

namespace TodoList.Web.Boards.Column.Card.Comment.Create;

public class CreateCommentValidation : Validator<CreateCommentRequest>
{
    public CreateCommentValidation()
    {
        RuleFor(x => x.CardId)
            .NotEmpty().WithMessage("Column ID is required.");

        RuleFor(x => x.Text)
            .NotEmpty()
            .MinimumLength(3);
    }
}
