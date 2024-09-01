using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace TodoList.Core.BoardAggregate;

public class Board : EntityBase<Guid>, IAggregateRoot
{
    // Parameterless constructor required by EF Core
    public Board() { }

    // Optional: additional constructor for manual use
    public Board(string name)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
    }

    public string Name { get; set; } = null!;
    public IQueryable<Column> Columns { get; set; } = null!;
}
