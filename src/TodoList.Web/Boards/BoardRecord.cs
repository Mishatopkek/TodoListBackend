using TodoList.Core.BoardAggregate;

namespace TodoList.Web.Boards;

public record BoardRecord(Ulid id, string name, string normalizedName, IEquatable<Column> columns)
{
}
