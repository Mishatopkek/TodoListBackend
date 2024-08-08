using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Users.Login;

public class LoginUserRequest
{
    public const string Route = "/Login";

    [Required] public string Login { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
}
