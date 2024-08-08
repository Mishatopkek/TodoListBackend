using Ardalis.Result;
using Ardalis.SharedKernel;
using TodoList.Core.Interfaces;
using TodoList.Core.UserAggregate;
using TodoList.Core.UserAggregate.Specifications;

namespace TodoList.UseCases.Users.SignUp;

public class SignUpUserHandler(IRepository<User> repository, IPasswordService passwordService)
    : ICommandHandler<SignUpUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Check if the user is already registered
            if (await IsUserRegistered(request, cancellationToken))
            {
                return Result.Conflict();
            }

            // Create a new user and add to the database
            var newUser = CreateUser(request);
            var createdUser = await repository.AddAsync(newUser, cancellationToken);

            // Generate and return the JWT token
            var token = passwordService.GenerateJwtToken(createdUser);
            return Result.Success(token);
        }
        catch (Exception ex)
        {
            // Handle exceptions and return a failure result
            return Result.Error(ex.Message);
        }
    }

    private async Task<bool> IsUserRegistered(SignUpUserCommand request, CancellationToken cancellationToken)
    {
        var spec = new SignUpUserSpec(request.Name, request.Email);
        var existingUser = await repository.FirstOrDefaultAsync(spec, cancellationToken);
        return existingUser != null;
    }

    private User CreateUser(SignUpUserCommand request)
    {
        var id = Ulid.NewUlid().ToGuid();
        var salt = passwordService.GenerateSalt();
        var passwordHash = passwordService.HashPassword(request.Password, salt);
        return new User(id, request.Name, request.Email, "user", passwordHash, salt);
    }
}
