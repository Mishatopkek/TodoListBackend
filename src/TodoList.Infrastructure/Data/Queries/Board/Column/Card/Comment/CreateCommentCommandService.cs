using TodoList.UseCases.Boards.Column.Card.Comment.Create;

namespace TodoList.Infrastructure.Data.Queries.Board.Column.Card.Comment;

public class CreateCommentCommandService(AppDbContext db) : ICreateCommentService
{
    public async Task<Ulid> CreateAsync(Ulid cardId, Ulid userId, string text)
    {
        var id = Ulid.NewUlid();
        await db.Comments.AddAsync(new Core.BoardAggregate.Comment
        {
            Id = id.ToGuid(),
            CardId = cardId.ToGuid(),
            UserId = userId.ToGuid(),
            Text = text,
            Date = DateTime.UtcNow
        });

        await db.SaveChangesAsync();
        return id;
    }
}
