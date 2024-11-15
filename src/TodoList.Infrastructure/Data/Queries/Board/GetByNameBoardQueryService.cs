using Microsoft.EntityFrameworkCore;
using TodoList.Core.Extensions;
using TodoList.UseCases.Boards;
using TodoList.UseCases.Boards.GetByName;

namespace TodoList.Infrastructure.Data.Queries.Board;

public class GetByNameBoardQueryService(AppDbContext db) : IGetByNameBoardService
{
    public async Task<BoardDTO?> GetBoardAsync(string userName, string name, Ulid requestUserId)
    {
        Guid boardUserId = await db.Boards
            .Include(x => x.User)
            .Where(board => board.User.Name == userName && board.Name == name)
            .Select(x => x.UserId)
            .FirstOrDefaultAsync();
        if (boardUserId != requestUserId.ToGuid())
        {
            throw new UnauthorizedAccessException("User tried to access to a board that does not have access to them.");
        }

        Task<BoardDTO?> response = db
            .Boards
            .Include(board => board.User)
            .Include(board => board.Columns)
            .ThenInclude(column => column.Cards)
            .ThenInclude(card => card.Comments)
            .AsNoTracking()
            .AsSplitQuery()
            .Where(board => board.Name == name && board.User.Name == userName)
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
                                            card.Title,
                                            card.Comments.OrderByDescending(comment => comment.Date)
                                                .Select(comment =>
                                                    new CommentDTO(
                                                        comment.Id.ToUlid(),
                                                        card.Id.ToUlid(),
                                                        column.Id.ToUlid(),
                                                        board.Id.ToUlid(),
                                                        comment.UserId.ToUlid(),
                                                        comment.Text,
                                                        comment.Date
                                                    ))))))))
            .FirstOrDefaultAsync();
        return await response;
    }
}
