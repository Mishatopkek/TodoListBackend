namespace TodoList.Web.Boards.Column.Order;

public class OrderColumnRequest
{
    public const string Route = "Board/Column/Order";

    public Ulid ColumnId { get; set; }
    public int Position { get; set; }
}
