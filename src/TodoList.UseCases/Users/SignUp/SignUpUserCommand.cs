using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Users.SignUp;

public record SignUpUserCommand(string Name, string Email, string Password) : ICommand<Result<string>>;
