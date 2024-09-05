using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using TodoList.Core.UserAggregate;

namespace TodoList.Core.BoardAggregate;

public class Board : EntityBase<Guid>, IAggregateRoot
{
    // Parameterless constructor required by EF Core
    public Board() { }

    // Optional: additional constructor for manual use
    public Board(Guid userId, User user, string name, string title)
    {
        UserId = Guard.Against.NullOrEmpty(userId, nameof(userId));
        User = Guard.Against.Null(user, nameof(user));
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
        Title = Guard.Against.NullOrEmpty(title, nameof(title));
    }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public IQueryable<Column> Columns { get; set; } = null!;
}
