using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.UseCases.Cards;
using TodoList.UseCases.Cards.List;

namespace TodoList.Web.Boards.List;

public class ListBoard(IMediator mediator) : EndpointWithoutRequest<ListBoardResponse>
{
    public override void Configure()
    {
        Get("Board/Cards");
        AllowAnonymous();
        Summary(s =>
        {
            s.Description = "Create a new Contributor. A valid name is required.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Result<IEnumerable<CardDto>> result = await mediator.Send(new ListCardsQuery(null, null), ct);

        if (result.IsSuccess)
        {
            Response =
                new ListBoardResponse() /*{Cards = result.Value.Select(c => new CardRecord(c.Id, c.Name)).ToList()}*/;
        }
    }
}
