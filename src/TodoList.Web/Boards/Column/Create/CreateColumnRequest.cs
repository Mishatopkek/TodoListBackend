using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Boards.Column.Create;

public class CreateColumnRequest
{
    public const string Route = "Board/Column";

    [Required] public Ulid BoardId { get; set; }
    [Required] public string? Title { get; set; }
    public bool IsAlwaysVisibleAddCardButton { get; set; } = false;
}
