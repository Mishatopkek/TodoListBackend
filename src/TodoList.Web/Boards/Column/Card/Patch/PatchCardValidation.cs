using FastEndpoints;
using FluentValidation;

namespace TodoList.Web.Boards.Column.Card.Patch;

public class PatchCardValidation : Validator<PatchCardRequest>
{
    public PatchCardValidation()
    {
        RuleFor(x => x.CardId)
            .NotEmpty().WithMessage("Card ID is required.");
    }
}
