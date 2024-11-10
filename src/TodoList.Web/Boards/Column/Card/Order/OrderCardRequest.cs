namespace TodoList.Web.Boards.Column.Card.Order;

public class OrderCardRequest
{
    public const string Route = "Board/Column/Card/Order";

    public Ulid CardId { get; set; }
    public Ulid DestinationColumnId { get; set; }
    public int Position { get; set; }
}
