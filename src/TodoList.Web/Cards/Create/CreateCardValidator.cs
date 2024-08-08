using FastEndpoints;
using FluentValidation;
using TodoList.Infrastructure.Data.Config;

namespace TodoList.Web.Cards.Create;

public class CreateCardValidator : Validator<CreateCardRequest>
{
    public CreateCardValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MinimumLength(2)
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
    }
}
