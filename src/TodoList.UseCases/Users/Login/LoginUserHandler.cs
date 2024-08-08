using Ardalis.Result;
using Ardalis.SharedKernel;
using Ardalis.Specification;
using TodoList.Core.Interfaces;
using TodoList.Core.UserAggregate;
using TodoList.Core.UserAggregate.Specifications;

namespace TodoList.UseCases.Users.Login;

public class LoginUserHandler(IRepositoryBase<User> repository, IPasswordService passwordService)
    : IQueryHandler<LoginUserQuery, Result<UserDTO>>
{
    public async Task<Result<UserDTO>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var spec = new LoginUserSpec(request.Login);
        var entity = await repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (entity == null)
        {
            return Result.NotFound();
        }

        var saltedRequestPassword = request.Password + Environment.GetEnvironmentVariable("PASSWORD_SALT_SECRET");
        var enteredHashedPassword = passwordService.HashPassword(saltedRequestPassword, entity.PasswordSalt);

        var arePasswordsEqual = passwordService.ArePasswordsEqual(enteredHashedPassword, entity.Password);

        if (!arePasswordsEqual)
        {
            return Result.NotFound();
        }

        var token = passwordService.GenerateJwtToken(entity);

        return new UserDTO(token);
    }
}
