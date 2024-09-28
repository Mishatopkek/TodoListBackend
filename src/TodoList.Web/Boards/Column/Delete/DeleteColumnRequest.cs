using Microsoft.Build.Framework;

namespace TodoList.Web.Boards.Column.Delete;

public class DeleteColumnRequest
{
    public const string Route = "Board/Column";

    [Required] public Ulid ColumnId { get; set; }
}
