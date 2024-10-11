using Microsoft.EntityFrameworkCore;
using TodoList.UseCases.Boards.Column.Order;

namespace TodoList.Infrastructure.Data.Queries.Board.Column;

public class OrderColumnCommandService(AppDbContext db) : IOrderColumnService
{
    public async Task UpdateColumnOrder(Ulid columnId, int newPosition)
    {
        var columnGuid = columnId.ToGuid();

        // Fetch the board ID and the current order of the column
        var columnData = await db.Columns
            .Where(column => column.Id == columnGuid)
            .Select(column => new {column.BoardId, column.Order})
            .FirstOrDefaultAsync();

        if (columnData == null)
        {
            throw new InvalidOperationException("Column not found");
        }

        var currentPosition = columnData.Order;

        // Only update if the position has actually changed
        if (currentPosition == newPosition)
        {
            return;
        }

        // Move up or down: shift the positions of the other columns
        if (currentPosition < newPosition)
        {
            await db.Columns
                .Where(column => column.BoardId == columnData.BoardId &&
                                 column.Order > currentPosition && column.Order <= newPosition)
                .ForEachAsync(column => column.Order--);
        }
        else
        {
            await db.Columns
                .Where(column => column.BoardId == columnData.BoardId &&
                                 column.Order >= newPosition && column.Order < currentPosition)
                .ForEachAsync(column => column.Order++);
        }

        // Update the order of the moved column
        Core.BoardAggregate.Column? movedColumn = await db.Columns.FirstOrDefaultAsync(c => c.Id == columnGuid);
        if (movedColumn != null)
        {
            movedColumn.Order = newPosition;
        }

        await db.SaveChangesAsync();
    }
}
