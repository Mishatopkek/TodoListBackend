using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Card.Patch;

public record PatchCardCommand(Ulid CardId, string? Title, string? Description) : ICommand<Result<bool>>;
