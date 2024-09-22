namespace TodoList.UseCases.Boards;

public record CommentDTO(Ulid Id, Ulid CardId, Ulid ColumnId, Ulid BoardId, Ulid UserId, string Text, DateTime Date);
