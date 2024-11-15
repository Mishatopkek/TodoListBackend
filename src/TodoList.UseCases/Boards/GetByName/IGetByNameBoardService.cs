namespace TodoList.UseCases.Boards.GetByName;

public interface IGetByNameBoardService
{
    Task<BoardDTO?> GetBoardAsync(string userName, string name, Ulid requestUserId);
}
