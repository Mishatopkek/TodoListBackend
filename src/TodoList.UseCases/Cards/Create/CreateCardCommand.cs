using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Cards.Create;

public record CreateCardCommand(string Name) : ICommand<Result<Ulid>>;
