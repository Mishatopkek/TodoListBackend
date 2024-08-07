using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace TodoList.Core.CardAggregate;

public class Card(string name) : EntityBase<Guid>, IAggregateRoot
{
    public string Name { get; set; } = Guard.Against.NullOrEmpty(name, nameof(name));

    public void UpdateName(string newName)
    {
        Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
    }
}
