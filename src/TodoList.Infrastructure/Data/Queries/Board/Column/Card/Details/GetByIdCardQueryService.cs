using Microsoft.EntityFrameworkCore;
using TodoList.Core.Extensions;
using TodoList.UseCases.Boards;
using TodoList.UseCases.Boards.Column.Card.Details.GetByIdCard;

namespace TodoList.Infrastructure.Data.Queries.Board.Column.Card.Details;

public class GetByIdCardQueryService(AppDbContext db) : IGetByIdCardDetailsService
{
    public async Task<CardDetailsDTO> GetCardDetailsAsync(Ulid id)
    {
        CardDetailsDTO cardDetails = await db.Cards
            .Include(card => card.Comments)
            .Include(card => card.Column)
            .Where(card => card.Id == id.ToGuid())
            .Select(card =>
                new CardDetailsDTO(
                    card.Description ?? "",
                    card.Comments.Select(comment =>
                        new CommentDTO(
                            comment.Id.ToUlid(),
                            card.Id.ToUlid(),
                            card.ColumnId.ToUlid(),
                            card.Column.BoardId.ToUlid(),
                            comment.UserId.ToUlid(),
                            comment.Text,
                            comment.Date))))
            .FirstAsync();
        return cardDetails;
    }
}
