namespace TodoList.UseCases.Boards.Column.Card.Create;

public interface ICreateCardService
{
    Task<Ulid> CreateAsync(Ulid columnId, string title);
}
