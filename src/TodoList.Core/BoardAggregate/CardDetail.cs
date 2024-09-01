using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace TodoList.Core.BoardAggregate;

public class CardDetails : EntityBase<Guid>, IAggregateRoot
{
    public CardDetails()
    {
    }

    public CardDetails(Guid cardId, Card card, string description)
    {
        CardId = Guard.Against.Null(cardId, nameof(cardId));
        Card = Guard.Against.Null(card, nameof(card));
        Description = Guard.Against.NullOrEmpty(description, nameof(description));
    }

    public Guid CardId { get; set; } = Guid.Empty;
    public Card Card { get; set; } = null!;

    public string Description { get; set; } = null!;
}
