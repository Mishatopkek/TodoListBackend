using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Card.Create;

public class CreateCardHandler(ICreateCardService service) : ICommandHandler<CreateCardCommand, Result<Ulid>>
{
    public async Task<Result<Ulid>> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        Ulid title = await service.CreateAsync(request.ColumnId, request.Title);
        return Result.Success(title);
    }
}
