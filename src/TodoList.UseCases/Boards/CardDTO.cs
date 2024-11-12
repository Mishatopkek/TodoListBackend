namespace TodoList.UseCases.Boards;

public record CardDTO(Ulid Id, Ulid ColumnId, Ulid BoardId, string Title, IEnumerable<CommentDTO> Comments);
