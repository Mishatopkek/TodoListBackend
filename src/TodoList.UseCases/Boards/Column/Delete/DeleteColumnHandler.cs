using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Delete;

public class DeleteColumnHandler(IDeleteColumnService service) : ICommandHandler<DeleteColumnCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteColumnCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await service.DeleteAsync(request.ColumnId, request.UserId);
        }
        catch (Exception e)
        {
            return Result.Error(e.Message);
        }

        return Result.Success(true);
    }
}
