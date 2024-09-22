namespace TodoList.UseCases.Boards;

public record BoardDTO(Ulid Id, string Name, string Title, string Author, IEnumerable<ColumnDTO> Columns);
