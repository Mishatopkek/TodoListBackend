using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoList.Core.Extensions;
using TodoList.UseCases.Boards.Column.Create;

namespace TodoList.Infrastructure.Data.Queries.Board.Column;

public class CreateColumnCommandService(AppDbContext db) : ICreateColumnService
{
    public async Task<Ulid> CreateAsync(Ulid boardId, string title, bool isAlwaysVisibleAddCardButton)
    {
        var boardIdGuid = boardId.ToGuid();
        var columnCount = await db.Boards
            .Where(board => board.Id == boardIdGuid)
            .Select(board => board.Columns.Count)
            .FirstOrDefaultAsync();

        EntityEntry<Core.BoardAggregate.Column> column = await db.Columns.AddAsync(
            new Core.BoardAggregate.Column
            {
                Id = Ulid.NewUlid().ToGuid(),
                BoardId = boardIdGuid,
                Title = title,
                IsAlwaysVisibleAddCardButton = isAlwaysVisibleAddCardButton,
                Order = columnCount + 1
            });

        await db.SaveChangesAsync();
        return column.Entity.Id.ToUlid();
    }
}
