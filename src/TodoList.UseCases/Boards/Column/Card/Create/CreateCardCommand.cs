using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Card.Create;

public record CreateCardCommand(Ulid ColumnId, string Title) : ICommand<Result<Ulid>>;
