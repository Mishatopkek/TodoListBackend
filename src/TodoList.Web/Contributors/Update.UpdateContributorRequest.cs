using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Contributors;

public class UpdateContributorRequest
{
    public const string Route = "/Contributors/{ContributorId:int}";

    public int ContributorId { get; set; }

    [Required] public int Id { get; set; }

    [Required] public string? Name { get; set; }

    public static string BuildRoute(int contributorId)
    {
        return Route.Replace("{ContributorId:int}", contributorId.ToString());
    }
}
