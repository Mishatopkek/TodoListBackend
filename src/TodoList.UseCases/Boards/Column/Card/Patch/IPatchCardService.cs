namespace TodoList.UseCases.Boards.Column.Card.Patch;

public interface IPatchCardService
{
    Task PatchAsync(Ulid cardId, string? title, string? description);
}
