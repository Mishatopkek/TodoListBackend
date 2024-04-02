using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Cards.Create;

public class CreateCardRequest
{
    public const string Route = "/Cards";

    [Required] public string? Name { get; set; }
}
