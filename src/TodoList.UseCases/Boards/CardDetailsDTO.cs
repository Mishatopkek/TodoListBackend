namespace TodoList.UseCases.Boards;

public record CardDetailsDTO(string Description, IEnumerable<CommentDTO> Comments);
