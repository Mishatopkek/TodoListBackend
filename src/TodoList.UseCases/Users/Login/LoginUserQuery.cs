using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Users.Login;

public record LoginUserQuery(string Login, string Password) : IQuery<Result<UserDTO>>;
