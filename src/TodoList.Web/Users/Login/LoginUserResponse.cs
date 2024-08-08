namespace TodoList.Web.Users.Login;

public class LoginUserResponse(string token)
{
    public string Token { get; set; } = token;
}
