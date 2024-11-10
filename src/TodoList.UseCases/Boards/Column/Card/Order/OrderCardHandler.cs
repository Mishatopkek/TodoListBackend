using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Card.Order;

public class OrderCardHandler(IOrderCardService service) : ICommandHandler<OrderCardCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(OrderCardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await service.OrderAsync(request.CardId, request.DestinationColumnId, request.Position);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result.Error(e.Message);
        }

        return Result.Success(true);
    }
}
