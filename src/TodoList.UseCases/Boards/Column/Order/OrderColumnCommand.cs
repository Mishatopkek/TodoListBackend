using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Order;

public record OrderColumnCommand(Ulid ColumnId, int NewPosition) : ICommand<Result<bool>>;
