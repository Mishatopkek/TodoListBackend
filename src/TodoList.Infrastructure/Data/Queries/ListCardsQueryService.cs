using Microsoft.EntityFrameworkCore;
using TodoList.UseCases.Cards;
using TodoList.UseCases.Cards.List;

namespace TodoList.Infrastructure.Data.Queries;

public class ListCardsQueryService(AppDbContext db) : IListCardsQueryService
{
    public async Task<IEnumerable<CardDto>> ListAsync()
    {
        var result = await db.Cards
            .FromSqlRaw("SELECT Id, Name FROM Cards")
            .Select(c => new CardDto(c.Id, c.Name))
            .ToListAsync();

        return result;
    }
}
