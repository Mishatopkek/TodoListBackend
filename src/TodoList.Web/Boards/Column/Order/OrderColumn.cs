using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.UseCases.Boards.Column.Order;

namespace TodoList.Web.Boards.Column.Order;

public class OrderColumn(IMediator mediator) : Endpoint<OrderColumnRequest>
{
    public override void Configure()
    {
        Post(OrderColumnRequest.Route);

        Summary(s =>
        {
            //TODO: Add summary
        });
    }

    public override async Task HandleAsync(OrderColumnRequest req, CancellationToken ct)
    {
        Result<bool> response = await mediator.Send(new OrderColumnCommand(req.ColumnId, req.Position), ct);

        if (response.IsSuccess)
        {
            await SendOkAsync(ct);
        }
        else
        {
            await SendErrorsAsync(StatusCodes.Status400BadRequest, ct);
        }
    }
}
