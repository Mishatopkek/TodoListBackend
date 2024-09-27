using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Create;

public class CreateColumnHandler(ICreateColumnService service) : ICommandHandler<CreateColumnCommand, Result<Ulid>>
{
    public async Task<Result<Ulid>> Handle(CreateColumnCommand request, CancellationToken cancellationToken)
    {
        Ulid id = await service.CreateAsync(request.BoardId, request.Title, request.ShowAddCardByDefault);
        return Result.Success(id);
    }
}
