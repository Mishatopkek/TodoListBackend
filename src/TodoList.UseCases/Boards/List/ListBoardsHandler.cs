using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.List;

public class ListBoardsHandler(IListBoardQueryService query)
    : IQueryHandler<ListBoardsQuery, Result<IEnumerable<BoardDto>>>
{
    public async Task<Result<IEnumerable<BoardDto>>> Handle(ListBoardsQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<BoardDto> result = await query.ListAsync();

        return Result.Success(result);
    }
}
