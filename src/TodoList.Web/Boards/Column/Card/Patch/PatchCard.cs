using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.UseCases.Boards.Column.Card.Patch;

namespace TodoList.Web.Boards.Column.Card.Patch;

public class PatchCard(IMediator mediator) : Endpoint<PatchCardRequest>
{
    public override void Configure()
    {
        Patch(PatchCardRequest.Route);

        Summary(s =>
        {
            //TODO add summary
        });
    }

    public override async Task HandleAsync(PatchCardRequest req, CancellationToken ct)
    {
        Result<bool> response =
            await mediator.Send(new PatchCardCommand(req.CardId, req.Title, req.Description), ct);

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
