using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Create;

public record CreateColumnCommand(Ulid BoardId, string Title, bool IsAlwaysVisibleAddCardButton, Ulid userId)
    : ICommand<Result<Ulid>>;
