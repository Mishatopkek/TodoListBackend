using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using TodoList.Core.UserAggregate;

namespace TodoList.Core.BoardAggregate;

public class Comment : EntityBase<Guid>, IAggregateRoot
{
    public Comment()
    {
    }

    public Comment(Guid cardId, Card card, Guid userId, User user, string text, DateTime date)
    {
        CardId = Guard.Against.Null(cardId, nameof(cardId));
        Card = Guard.Against.Null(card, nameof(card));
        UserId = Guard.Against.Null(userId, nameof(userId));
        User = Guard.Against.Null(user, nameof(user));
        Text = Guard.Against.NullOrEmpty(text, nameof(text));
        Date = Guard.Against.Null(date, nameof(date));
    }

    public Guid CardId { get; set; } = Guid.Empty;
    public Card Card { get; set; } = null!;

    public Guid UserId { get; set; } = Guid.Empty;
    public User User { get; set; } = null!;

    public string Text { get; set; } = null!;
    public DateTime Date { get; set; } = DateTime.MinValue;
}
