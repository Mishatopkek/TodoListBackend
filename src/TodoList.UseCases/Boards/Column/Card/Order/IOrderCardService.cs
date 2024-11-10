namespace TodoList.UseCases.Boards.Column.Card.Order;

public interface IOrderCardService
{
    Task OrderAsync(Ulid cardId, Ulid destinationColumnId, int position);
}
