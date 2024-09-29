using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.UseCases.Boards.Column.Patch;

namespace TodoList.Web.Boards.Column.Patch;

public class PatchColumn(IMediator mediator) : Endpoint<PatchColumnRequest>
{
    public override void Configure()
    {
        Patch(PatchColumnRequest.Route);

        Summary(s =>
        {
            //TODO: add summary
        });
    }

    public override async Task HandleAsync(PatchColumnRequest req, CancellationToken ct)
    {
        Result<bool> response =
            await mediator.Send(new PatchColumnCommand(req.ColumnId, req.Title, req.IsAlwaysVisibleAddCardButton), ct);

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
