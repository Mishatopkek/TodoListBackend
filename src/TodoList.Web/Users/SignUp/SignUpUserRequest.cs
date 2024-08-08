using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Users.SignUp;

public class SignUpUserRequest
{
    public const string Route = "/User/SignUp";

    [Required] public string Name { get; set; } = null!;
    [Required] public string Email { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
}
