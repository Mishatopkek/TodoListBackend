using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoList.Core.Extensions;
using TodoList.UseCases.Boards.Column.Card.Create;

namespace TodoList.Infrastructure.Data.Queries.Board.Column.Card;

public class CreateCardCommandService(AppDbContext db) : ICreateCardService
{
    public async Task<Ulid> CreateAsync(Ulid columnId, string title)
    {
        EntityEntry<Core.BoardAggregate.Card> card = await db.Cards.AddAsync(new Core.BoardAggregate.Card
        {
            ColumnId = columnId.ToGuid(), Title = title
        });

        return card.Entity.Id.ToUlid();
    }
}
