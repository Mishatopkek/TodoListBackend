using Microsoft.EntityFrameworkCore;
using TodoList.Core.Extensions;
using TodoList.UseCases.Boards;
using TodoList.UseCases.Boards.GetByName;

namespace TodoList.Infrastructure.Data.Queries.Board;

public class GetByNameBoardQueryService(AppDbContext db) : IGetByNameBoardService
{
    public Task<BoardDTO?> GetBoardAsync(string name)
    {
        Task<BoardDTO?> response = db
            .Boards
            .Include(board => board.User)
            .Include(board => board.Columns)
            .ThenInclude(column => column.Cards)
            .AsNoTracking()
            .AsSplitQuery()
            .Where(board => board.Name == name)
            .Select(board =>
                new BoardDTO(
                    board.Id.ToUlid(),
                    board.Name,
                    board.Title,
                    board.User.Name,
                    board.Columns
                        .OrderBy(column => column.Order)
                        .Select(column =>
                            new ColumnDTO(
                                column.Id.ToUlid(),
                                board.Id.ToUlid(),
                                column.Title,
                                column.IsAlwaysVisibleAddCardButton,
                                column.Cards
                                    .OrderBy(card => card.Order)
                                    .Select(card =>
                                        new CardDTO(
                                            card.Id.ToUlid(),
                                            column.Id.ToUlid(),
                                            board.Id.ToUlid(),
                                            card.Title))))))
            .FirstOrDefaultAsync();
        return response;
    }
}
