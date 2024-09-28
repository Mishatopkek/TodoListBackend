using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Delete;

public record DeleteColumnCommand(Ulid ColumnId, Ulid UserId) : ICommand<Result<bool>>;
