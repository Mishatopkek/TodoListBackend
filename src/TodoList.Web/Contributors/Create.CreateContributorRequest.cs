using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Contributors;

public class CreateContributorRequest
{
    public const string Route = "/Contributors";

    [Required] public string? Name { get; set; }
}
