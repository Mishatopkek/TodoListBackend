using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Cards.List;

public class ListCardsHandler(IListCardsQueryService query)
    : IQueryHandler<ListCardsQuery, Result<IEnumerable<CardDto>>>
{
    public async Task<Result<IEnumerable<CardDto>>> Handle(ListCardsQuery request, CancellationToken cancellationToken)
    {
        var result = await query.ListAsync();

        return Result.Success(result);
    }
}
