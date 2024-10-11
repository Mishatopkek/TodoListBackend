namespace TodoList.UseCases.Boards.Column.Order;

public interface IOrderColumnService
{
    Task UpdateColumnOrder(Ulid columnId, int newPosition);
}
