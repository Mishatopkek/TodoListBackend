using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace TodoList.Core.UserAggregate;

public class User(Guid id, string name, string email, string role, byte[] password, byte[] passwordSalt)
    : EntityBase<Guid>, IAggregateRoot
{
    public new Guid Id { get; set; } = Guard.Against.Null(id, nameof(id));
    public string Name { get; set; } = Guard.Against.NullOrEmpty(name, nameof(name));
    public string Email { get; set; } = Guard.Against.NullOrEmpty(email, nameof(email));
    public string Role { get; set; } = Guard.Against.NullOrEmpty(role, nameof(role));
    public byte[] Password { get; set; } = Guard.Against.NullOrEmpty(password, nameof(password)).ToArray();
    public byte[] PasswordSalt { get; set; } = Guard.Against.NullOrEmpty(passwordSalt, nameof(passwordSalt)).ToArray();
}
