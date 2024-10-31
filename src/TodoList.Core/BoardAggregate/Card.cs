using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace TodoList.Core.BoardAggregate;

public class Card : EntityBase<Guid>, IAggregateRoot
{
    public Card()
    {
    }

    public Card(Guid columnId, Column column, string name)
    {
        ColumnId = Guard.Against.Null(columnId, nameof(columnId));
        Column = Guard.Against.Null(column, nameof(column));
        Title = Guard.Against.NullOrEmpty(name, nameof(name));
    }

    public Guid ColumnId { get; set; } = Guid.Empty;
    public Column Column { get; set; } = null!;

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public List<Comment> Comments { get; set; } = [];
}
