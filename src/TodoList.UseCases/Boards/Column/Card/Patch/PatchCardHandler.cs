using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Card.Patch;

public class PatchCardHandler(IPatchCardService service) : ICommandHandler<PatchCardCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(PatchCardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await service.PatchAsync(request.CardId, request.Title, request.Description);
        }
        catch (Exception e)
        {
            return Result.Error(e.Message);
        }

        return Result.Success(true);
    }
}
