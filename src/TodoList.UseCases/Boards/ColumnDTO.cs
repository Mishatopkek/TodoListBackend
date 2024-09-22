namespace TodoList.UseCases.Boards;

public record ColumnDTO(Ulid Id, Ulid BoardId, string Title, bool ShowAddCardByDefault, IEnumerable<CardDTO> Cards);
