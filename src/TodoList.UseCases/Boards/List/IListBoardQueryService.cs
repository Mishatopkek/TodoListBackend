namespace TodoList.UseCases.Boards.List;

public interface IListBoardQueryService
{
    Task<IEnumerable<BoardDto>> ListAsync();
}
