namespace TodoList.Core.UserAggregate;

public class JwtUserModel
{
    public Ulid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
}
