using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Boards.Create;

public class CreateBoardRequest
{
    public const string Route = "/Board";

    [Required] public string? Title { get; set; }
    [Required] public string? Name { get; set; }
}
