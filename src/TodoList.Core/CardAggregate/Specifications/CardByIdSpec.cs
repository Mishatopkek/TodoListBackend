using Ardalis.Specification;

namespace TodoList.Core.CardAggregate.Specifications;

public sealed class CardByIdSpec : Specification<Card>
{
    public CardByIdSpec(int cardId)
    {
        Query.Where(card => card.Id == cardId);
    }
}
