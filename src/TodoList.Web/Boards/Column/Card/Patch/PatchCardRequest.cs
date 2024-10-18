namespace TodoList.Web.Boards.Column.Card.Patch;

public class PatchCardRequest
{
    public const string Route = "Board/Column/Card";

    public Ulid CardId { get; set; }
    public string? Title { get; set; }
}
