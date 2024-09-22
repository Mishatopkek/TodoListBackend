using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.UseCases.Boards;
using TodoList.UseCases.Boards.GetByName;

namespace TodoList.Web.Boards.GetByName;

public class GetByNameBoard(IMediator mediator) : Endpoint<GetByNameBoardRequest, GetByNameBoardResponse>
{
    public override void Configure()
    {
        Get(GetByNameBoardRequest.Route);
    }

    public override async Task HandleAsync(GetByNameBoardRequest req, CancellationToken ct)
    {
        Result<BoardDTO> board = await mediator.Send(new GetByNameBoardQuery(req.Name), ct);

        switch (board.Status)
        {
            case ResultStatus.Ok:
                await SendOkAsync(new GetByNameBoardResponse(board), ct);
                break;
            case ResultStatus.Error:
                await SendErrorsAsync(cancellation: ct);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
