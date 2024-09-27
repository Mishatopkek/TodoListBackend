namespace TodoList.Web.Boards;

public record BoardRecord(Ulid id, string name, string normalizedName, IEquatable<Core.BoardAggregate.Column> columns)
{
}
