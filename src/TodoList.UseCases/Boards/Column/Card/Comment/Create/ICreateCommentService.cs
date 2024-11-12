namespace TodoList.UseCases.Boards.Column.Card.Comment.Create;

public interface ICreateCommentService
{
    Task<Ulid> CreateAsync(Ulid cardId, Ulid userId, string text);
}
