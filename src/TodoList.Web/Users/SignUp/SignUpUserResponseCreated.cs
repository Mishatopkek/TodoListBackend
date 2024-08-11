namespace TodoList.Web.Users.SignUp;

public class SignUpUserResponseCreated(string token)
{
    public string Token { get; set; } = token;
}
