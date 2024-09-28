namespace TodoList.UseCases.Boards.Column.Delete;

public interface IDeleteColumnService
{
    Task DeleteAsync(Ulid columnId, Ulid userId);
}
