using TodoList.UseCases.Boards.Column.Patch;

namespace TodoList.Infrastructure.Data.Queries.Board.Column;

public class PatchColumnCommandService(AppDbContext db) : IPatchColumnService
{
    public async Task PatchAsync(Ulid columnId, string? title, bool? isAlwaysVisibleAddCardButton)
    {
        Core.BoardAggregate.Column? column = await db.Columns.FindAsync(columnId.ToGuid());

        if (column == null)
        {
            throw new KeyNotFoundException($"Column with ID {columnId} was not found.");
        }

        if (!string.IsNullOrEmpty(title))
        {
            column.Title = title;
        }

        if (isAlwaysVisibleAddCardButton.HasValue)
        {
            column.IsAlwaysVisibleAddCardButton = isAlwaysVisibleAddCardButton.Value;
        }

        await db.SaveChangesAsync();
    }
}
