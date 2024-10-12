using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.UseCases.Boards.Column.Card.Create;

namespace TodoList.Web.Boards.Column.Card.Create;

public class CreateCard(IMediator mediator) : Endpoint<CreateCardRequest, CreateCardResponse>
{
    public override void Configure()
    {
        Post(CreateCardRequest.Route);

        Summary(s =>
        {
            //TODO: Add summary
        });
    }

    public override async Task HandleAsync(CreateCardRequest req, CancellationToken ct)
    {
        Result<Ulid> response = await mediator.Send(new CreateCardCommand(req.ColumnId, req.Title));

        if (response.IsSuccess)
        {
            await SendAsync(new CreateCardResponse {Id = response.Value}, cancellation: ct);
        }
    }
}
