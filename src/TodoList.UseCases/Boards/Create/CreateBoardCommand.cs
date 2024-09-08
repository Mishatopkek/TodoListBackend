using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Create;

public record CreateBoardCommand(string Title, string Name, Guid UserId) : ICommand<Result<bool>>;
