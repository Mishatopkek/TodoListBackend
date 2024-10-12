namespace TodoList.Web.Boards.Column.Card.Create;

public class CreateCardRequest
{
    public const string Route = "Board/Column/Card";

    public Ulid ColumnId { get; set; }
    public string Title { get; set; } = null!;
}
