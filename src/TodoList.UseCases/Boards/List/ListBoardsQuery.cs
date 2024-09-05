using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.List;

public record ListBoardsQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<BoardDto>>>;
