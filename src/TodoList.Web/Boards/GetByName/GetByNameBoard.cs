using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.Core.Extensions;
using TodoList.Core.UserAggregate;
using TodoList.UseCases.Boards;
using TodoList.UseCases.Boards.GetByName;

namespace TodoList.Web.Boards.GetByName;

public class GetByNameBoard(IMediator mediator) : Endpoint<GetByNameBoardRequest, GetByNameBoardResponse>
{
    public override void Configure()
    {
        Post(GetByNameBoardRequest.Route);
    }

    public override async Task HandleAsync(GetByNameBoardRequest req, CancellationToken ct)
    {
        JwtUserModel user = User.GetJwtUser();
        Result<BoardDTO> board = await mediator.Send(new GetByNameBoardQuery(req.UserName, req.Name, user.UserId), ct);

        switch (board.Status)
        {
            case ResultStatus.Ok:
                await SendOkAsync(new GetByNameBoardResponse(board), ct);
                break;
            case ResultStatus.Forbidden:
                await SendForbiddenAsync(ct);
                break;
            case ResultStatus.Error:
                await SendErrorsAsync(cancellation: ct);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
