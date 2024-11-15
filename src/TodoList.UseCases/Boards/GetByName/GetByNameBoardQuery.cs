using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.GetByName;

public record GetByNameBoardQuery(string UserName, string Name, Ulid UserId) : IQuery<Result<BoardDTO>>;
