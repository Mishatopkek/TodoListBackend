using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Boards.Column.Patch;

public class PatchColumnRequest
{
    public const string Route = "Board/Column";

    [Required] public Ulid ColumnId { get; set; }
    public string? Title { get; set; }
    public bool? IsAlwaysVisibleAddCardButton { get; set; }
}
