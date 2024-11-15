using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Boards.GetByName;

public class GetByNameBoardRequest
{
    public const string Route = "/Board/GetByName";

    [Required] public string UserName { get; set; } = null!;
    [Required] public string Name { get; set; } = null!;
}
