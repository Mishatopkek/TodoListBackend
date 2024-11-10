using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Card.Order;

public record OrderCardCommand(Ulid CardId, Ulid DestinationColumnId, int Position) : ICommand<Result<bool>>;
