using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.GetByName;

public class GetByNameBoardHandler(IGetByNameBoardService query) :
    IQueryHandler<GetByNameBoardQuery, Result<BoardDTO>>
{
    private const string BoardExist = "Board is already exist. Try another name";

    public async Task<Result<BoardDTO>> Handle(GetByNameBoardQuery request, CancellationToken cancellationToken)
    {
        BoardDTO? board = await query.GetBoardAsync(request.Name);

        return board == null ? Result.Error(BoardExist) : Result.Success(board);
    }
}
