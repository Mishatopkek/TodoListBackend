using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoList.Core.Extensions;
using TodoList.UseCases.Boards.Column.Card.Create;

namespace TodoList.Infrastructure.Data.Queries.Board.Column.Card;

public class CreateCardCommandService(AppDbContext db) : ICreateCardService
{
    public async Task<Ulid> CreateAsync(Ulid columnId, string title)
    {
        var columnIdGuid = columnId.ToGuid();
        var columnCount = await db.Columns
            .Where(column => column.Id == columnIdGuid)
            .Select(column => column.Cards.Count)
            .FirstOrDefaultAsync();

        EntityEntry<Core.BoardAggregate.Card> card = await db.Cards.AddAsync(new Core.BoardAggregate.Card
        {
            Id = Ulid.NewUlid().ToGuid(), ColumnId = columnIdGuid, Title = title, Order = columnCount + 1
        });

        await db.SaveChangesAsync();
        return card.Entity.Id.ToUlid();
    }
}
