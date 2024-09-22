using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.GetByName;

public record GetByNameBoardQuery(string Name) : IQuery<Result<BoardDTO>>;
