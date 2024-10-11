using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Order;

public class OrderColumnHandler(IOrderColumnService service) : ICommandHandler<OrderColumnCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(OrderColumnCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await service.UpdateColumnOrder(request.ColumnId, request.NewPosition);
        }
        catch (Exception e)
        {
            return Result.Error(e.Message);
        }

        return Result.Success(true);
    }
}
