using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.UseCases.Boards.Column.Card.Order;

namespace TodoList.Web.Boards.Column.Card.Order;

public class OrderCard(IMediator mediator) : Endpoint<OrderCardRequest>
{
    public override void Configure()
    {
        Post(OrderCardRequest.Route);

        Summary(s =>
        {
            //TODO add summary
        });
    }

    public override async Task HandleAsync(OrderCardRequest req, CancellationToken ct)
    {
        Result<bool> response =
            await mediator.Send(new OrderCardCommand(req.CardId, req.DestinationColumnId, req.Position), ct);

        if (response.IsSuccess)
        {
            await SendOkAsync(ct);
            return;
        }

        await SendErrorsAsync(400, ct);
    }
}
