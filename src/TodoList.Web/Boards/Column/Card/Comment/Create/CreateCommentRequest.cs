namespace TodoList.Web.Boards.Column.Card.Comment.Create;

public class CreateCommentRequest
{
    public const string Route = "Board/Column/Card/Comment";

    public Ulid CardId { get; set; }
    public string Text { get; set; } = null!;
}
