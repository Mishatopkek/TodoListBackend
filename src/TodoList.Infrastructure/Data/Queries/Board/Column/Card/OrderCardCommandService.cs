using Microsoft.EntityFrameworkCore;
using TodoList.Core.Extensions;
using TodoList.UseCases.Boards.Column.Card.Order;

namespace TodoList.Infrastructure.Data.Queries.Board.Column.Card;

public class OrderCardCommandService(AppDbContext db) : IOrderCardService
{
    public async Task OrderAsync(Ulid cardId, Ulid destinationColumnId, int position)
    {
        Core.BoardAggregate.Card? cardData = await db.Cards.FirstOrDefaultAsync(card => card.Id == cardId.ToGuid());

        if (cardData == null)
        {
            throw new InvalidOperationException("Card was not found");
        }

        var ulidCardId = cardData.ColumnId.ToUlid();
        var destinationColumnIdGuid = destinationColumnId.ToGuid();

        if (destinationColumnId == ulidCardId && cardData.Order == position)
        {
            return;
        }

        if (destinationColumnId == ulidCardId)
        {
            if (cardData.Order < position)
            {
                await db.Cards
                    .Where(card => card.ColumnId == cardData.ColumnId &&
                                   card.Order > cardData.Order &&
                                   card.Order <= position)
                    .ForEachAsync(card => card.Order--);
            }
            else
            {
                await db.Cards
                    .Where(card => card.ColumnId == cardData.ColumnId &&
                                   card.Order >= position &&
                                   card.Order < cardData.Order)
                    .ForEachAsync(card => card.Order++);
            }
        }
        else
        {
            await db.Cards
                .Where(card => card.ColumnId == cardData.ColumnId &&
                               card.Order > cardData.Order)
                .ForEachAsync(card => card.Order--);

            await db.Cards
                .Where(card => card.ColumnId == destinationColumnIdGuid &&
                               card.Order >= position)
                .ForEachAsync(card => card.Order++);
            cardData.ColumnId = destinationColumnIdGuid;
        }

        cardData.Order = position;
        await db.SaveChangesAsync();
    }
}
