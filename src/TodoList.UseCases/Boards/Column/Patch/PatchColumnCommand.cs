using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Patch;

public record PatchColumnCommand(Ulid ColumnId, string? Title, bool? IsAlwaysVisibleAddCardButton)
    : ICommand<Result<bool>>;
