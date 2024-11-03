using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoList.Core.Extensions;
using TodoList.UseCases.Boards.Column.Create;

namespace TodoList.Infrastructure.Data.Queries.Board.Column;

public class CreateColumnCommandService(AppDbContext db) : ICreateColumnService
{
    public async Task<Ulid> CreateAsync(Ulid boardId, string title, bool isAlwaysVisibleAddCardButton)
    {
        EntityEntry<Core.BoardAggregate.Column> column = await db.Columns.AddAsync(
            new Core.BoardAggregate.Column
            {
                Id = Ulid.NewUlid().ToGuid(),
                BoardId = boardId.ToGuid(),
                Title = title,
                IsAlwaysVisibleAddCardButton = isAlwaysVisibleAddCardButton
            });

        await db.SaveChangesAsync();
        return column.Entity.Id.ToUlid();
    }
}
