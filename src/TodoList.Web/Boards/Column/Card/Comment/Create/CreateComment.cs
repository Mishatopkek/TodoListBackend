using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.Core.Extensions;
using TodoList.Core.UserAggregate;
using TodoList.UseCases.Boards.Column.Card.Comment.Create;

namespace TodoList.Web.Boards.Column.Card.Comment.Create;

public class CreateComment(IMediator mediator) : Endpoint<CreateCommentRequest, CreateCommentResponse>
{
    public override void Configure()
    {
        Post(CreateCommentRequest.Route);

        Summary(s =>
        {
            //TODO add summary
        });
    }

    public override async Task HandleAsync(CreateCommentRequest req, CancellationToken ct)
    {
        JwtUserModel user = User.GetJwtUser();
        Result<Ulid> response = await mediator.Send(new CreateCommentCommand(req.CardId, user.UserId, req.Text), ct);

        if (response.IsSuccess)
        {
            await SendAsync(new CreateCommentResponse {Id = response.Value}, cancellation: ct);
            return;
        }

        await SendErrorsAsync(400, ct);
    }
}
