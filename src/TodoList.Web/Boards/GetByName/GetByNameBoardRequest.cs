using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Boards.GetByName;

public class GetByNameBoardRequest
{
    public const string Route = "/Board/GetByName/{Name}";

    [Required] public string Name { get; set; } = null!;
}
