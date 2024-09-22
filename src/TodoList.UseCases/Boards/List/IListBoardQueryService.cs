namespace TodoList.UseCases.Boards.List;

public interface IListBoardQueryService
{
    Task<IEnumerable<BoardDTO>> ListAsync();
}
