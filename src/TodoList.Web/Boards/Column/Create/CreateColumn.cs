using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.Core.Extensions;
using TodoList.Core.UserAggregate;
using TodoList.UseCases.Boards.Column.Create;

namespace TodoList.Web.Boards.Column.Create;

public class CreateColumn(IMediator mediator) : Endpoint<CreateColumnRequest, CreateColumnResponse>
{
    public override void Configure()
    {
        Post(CreateColumnRequest.Route);

        Summary(s =>
        {
            //TODO add summary
        });
    }

    public override async Task HandleAsync(CreateColumnRequest req, CancellationToken ct)
    {
        JwtUserModel user = User.GetJwtUser();
        Result<Ulid> result =
            await mediator.Send(
                new CreateColumnCommand(req.BoardId, req.Title!, req.IsAlwaysVisibleAddCardButton, user.UserId),
                ct);

        switch (result.Status)
        {
            case ResultStatus.Ok:
                Response.ColumnId = result.Value;
                break;
            case ResultStatus.Forbidden:
                //TODO: add check if user is invited
                await SendForbiddenAsync(ct);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
