using Ardalis.Result;
using Ardalis.SharedKernel;
using Ardalis.Specification;
using TodoList.Core.Interfaces;
using TodoList.Core.UserAggregate;
using TodoList.Core.UserAggregate.Specifications;

namespace TodoList.UseCases.Users.Login;

public class LoginUserHandler(
    IRepositoryBase<User> repository,
    IPasswordService passwordService,
    IJwtService jwtService)
    : IQueryHandler<LoginUserQuery, Result<UserDTO>>
{
    public async Task<Result<UserDTO>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        LoginUserSpec spec = new(request.Login);
        User? entity = await repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (entity == null)
        {
            return Result.NotFound();
        }

        var enteredHashedPassword = passwordService.Hash(request.Password, entity.PasswordSalt);

        var arePasswordsEqual = passwordService.ArePasswordsEqual(enteredHashedPassword, entity.Password);

        if (!arePasswordsEqual)
        {
            return Result.NotFound();
        }

        var token = jwtService.GenerateJwtToken(entity);

        return new UserDTO(token);
    }
}
