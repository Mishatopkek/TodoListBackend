namespace TodoList.Web.Users.SignUp;

public class SignUpUserResponse(string token)
{
    public string Token { get; set; } = token;
}
