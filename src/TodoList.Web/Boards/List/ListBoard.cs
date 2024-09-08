using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.UseCases.Boards;
using TodoList.UseCases.Boards.List;

namespace TodoList.Web.Boards.List;

public class ListBoard(IMediator mediator) : EndpointWithoutRequest<IEnumerable<ListBoardResponse>>
{
    public override void Configure()
    {
        Get("Board/Cards");
        AllowAnonymous();
        Summary(s =>
        {
            s.Description = "Create a new Contributor. A valid name is required.";
            //TODO add request and error response examples
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Result<IEnumerable<BoardDto>> result = await mediator.Send(new ListBoardsQuery(null, null), ct);

        if (result.IsSuccess)
        {
            Response = result.Value.Select(card => new ListBoardResponse(card.Id, card.Name, card.Title, card.Author));
        }
    }
}
