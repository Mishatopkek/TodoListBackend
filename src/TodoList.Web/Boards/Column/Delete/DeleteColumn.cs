using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.Core.Extensions;
using TodoList.Core.UserAggregate;
using TodoList.UseCases.Boards.Column.Delete;

namespace TodoList.Web.Boards.Column.Delete;

public class DeleteColumn(IMediator mediator) : Endpoint<DeleteColumnRequest>
{
    public override void Configure()
    {
        Delete(DeleteColumnRequest.Route);

        Summary(s =>
        {
            //TODO add summary
        });
    }

    public override async Task HandleAsync(DeleteColumnRequest req, CancellationToken ct)
    {
        JwtUserModel user = User.GetJwtUser();
        Result<bool> result = await mediator.Send(new DeleteColumnCommand(req.ColumnId, user.UserId), ct);

        if (result.IsSuccess)
        {
            await SendOkAsync(ct);
        }
        else
        {
            await SendErrorsAsync(cancellation: ct);
        }
    }
}
