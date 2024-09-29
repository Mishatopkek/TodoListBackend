using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Patch;

public class PatchColumnHandler(IPatchColumnService service) : ICommandHandler<PatchColumnCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(PatchColumnCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await service.PatchAsync(request.ColumnId, request.Title, request.IsAlwaysVisibleAddCardButton);
        }
        catch (Exception e)
        {
            return Result.Error(e.Message);
        }

        return Result.Success(true);
    }
}
