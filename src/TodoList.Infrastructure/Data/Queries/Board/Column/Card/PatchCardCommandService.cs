﻿using TodoList.UseCases.Boards.Column.Card.Patch;

namespace TodoList.Infrastructure.Data.Queries.Board.Column.Card;

public class PatchCardCommandService(AppDbContext db) : IPatchCardService
{
    public async Task PatchAsync(Ulid cardId, string? title)
    {
        Core.BoardAggregate.Card? card = await db.Cards.FindAsync(cardId.ToGuid());

        if (card == null)
        {
            throw new KeyNotFoundException($"Card with ID {cardId} was not found.");
        }

        if (!string.IsNullOrEmpty(title))
        {
            card.Title = title;
        }

        await db.SaveChangesAsync();
    }
}