namespace TodoList.UseCases.Boards.GetByName;

public interface IGetByNameBoardService
{
    Task<BoardDTO?> GetBoardAsync(string name);
}
