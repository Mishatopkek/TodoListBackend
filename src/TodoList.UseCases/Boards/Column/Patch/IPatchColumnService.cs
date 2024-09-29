namespace TodoList.UseCases.Boards.Column.Patch;

public interface IPatchColumnService
{
    Task PatchAsync(Ulid columnId, string? title, bool? isAlwaysVisibleAddCardButton);
}
