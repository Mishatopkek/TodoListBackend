namespace TodoList.UseCases.Boards.Column.Create;

public interface ICreateColumnService
{
    Task<Ulid> CreateAsync(Ulid boardId, string title, bool showAddCardByDefault);
}
