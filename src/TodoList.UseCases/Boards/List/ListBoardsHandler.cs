using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.List;

public class ListBoardsHandler(IListBoardQueryService query)
    : IQueryHandler<ListBoardsQuery, Result<IEnumerable<BoardDTO>>>
{
    public async Task<Result<IEnumerable<BoardDTO>>> Handle(ListBoardsQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<BoardDTO> result = await query.ListAsync();

        return Result.Success(result);
    }
}
