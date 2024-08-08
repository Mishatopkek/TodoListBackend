namespace TodoList.Web.Users;

public record UserRecord(Ulid Id, string name, string email, string rule);
