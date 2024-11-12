using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.List;

public record ListBoardsQuery(Ulid UserId) : IQuery<Result<IEnumerable<BoardDTO>>>;
