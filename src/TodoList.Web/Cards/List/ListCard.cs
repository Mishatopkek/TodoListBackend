using FastEndpoints;
using MediatR;
using TodoList.UseCases.Cards.List;

namespace TodoList.Web.Cards.List;

/// <summary>
///     Get all cards
/// </summary>
public class ListCard(IMediator mediator) : EndpointWithoutRequest<ListCardResponse>
{
    public override void Configure()
    {
        Get("/Cards");
        AllowAnonymous();
        Summary(s =>
        {
            s.Description = "Create a new Contributor. A valid name is required.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await mediator.Send(new ListCardsQuery(null, null), ct);

        if (result.IsSuccess)
        {
            Response = new ListCardResponse {Cards = result.Value.Select(c => new CardRecord(c.Id, c.Name)).ToList()};
        }
    }
}
