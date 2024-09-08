using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.Core.Extensions;
using TodoList.Core.UserAggregate;
using TodoList.UseCases.Boards.Create;

namespace TodoList.Web.Boards.Create;

public class CreateBoard(IMediator mediator) : Endpoint<CreateBoardRequest>
{
    public override void Configure()
    {
        Post(CreateBoardRequest.Route);

        Summary(s =>
        {
            //TODO add summary
        });
    }

    public override async Task HandleAsync(CreateBoardRequest req, CancellationToken ct)
    {
        JwtUserModel user = User.GetJwtUser();
        Result<bool> result = await mediator.Send(new CreateBoardCommand(req.Title!, req.Name!, user.UserId), ct);

        switch (result.Status)
        {
            case ResultStatus.Created:
                await SendAsync(null, StatusCodes.Status201Created, ct);
                break;
            case ResultStatus.Invalid:
                await SendAsync(result.ToErrorResponse(), StatusCodes.Status409Conflict, ct);
                break;
        }
    }
}
