using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.GetByName;

public class GetByNameBoardHandler(IGetByNameBoardService query) :
    IQueryHandler<GetByNameBoardQuery, Result<BoardDTO>>
{
    private const string BoardExist = "Board is already exist. Try another name";

    public async Task<Result<BoardDTO>> Handle(GetByNameBoardQuery request, CancellationToken cancellationToken)
    {
        try
        {
            BoardDTO? board = await query.GetBoardAsync(request.UserName, request.Name, request.UserId);
            return board == null ? Result.Error(BoardExist) : Result.Success(board);
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine(e);
            return Result.Forbidden("You are not authorized to access this board");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result.Error(e.Message);
        }
    }
}
