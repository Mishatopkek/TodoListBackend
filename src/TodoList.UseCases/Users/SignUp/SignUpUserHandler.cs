using Ardalis.Result;
using Ardalis.SharedKernel;
using TodoList.Core.Interfaces;
using TodoList.Core.UserAggregate;
using TodoList.Core.UserAggregate.Specifications;

namespace TodoList.UseCases.Users.SignUp;

public class SignUpUserHandler(IRepository<User> repository, IPasswordService passwordService, IJwtService jwtService)
    : ICommandHandler<SignUpUserCommand, Result<string>>
{
    private const string UsernameExist = "User is already exist. Try another username";
    private const string EmailExist = "Email is already exist. Try another email";

    public async Task<Result<string>> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Check if the user is already registered
            var entity = await IsUserRegistered(request, cancellationToken);
            if (entity != null)
            {
                return FillUpPossibleReasons(request, entity);
            }

            // Create a new user and add to the database
            var newUser = CreateUser(request);
            var createdUser = await repository.AddAsync(newUser, cancellationToken);

            // Generate and return the JWT token
            var token = jwtService.GenerateJwtToken(createdUser);
            return Result<string>.Created(token);
        }
        catch (Exception ex)
        {
            // Handle exceptions and return a failure result
            return Result.Error(ex.Message);
        }
    }

    private async Task<User?> IsUserRegistered(SignUpUserCommand request, CancellationToken cancellationToken)
    {
        var spec = new SignUpUserSpec(request.Name, request.Email);
        var user = await repository.FirstOrDefaultAsync(spec, cancellationToken);
        return user;
    }

    private static Result<string> FillUpPossibleReasons(SignUpUserCommand request, User entity)
    {
        List<ValidationError> errors = [];

        if (entity.Name == request.Name)
        {
            errors.Add(new ValidationError(nameof(request.Name), UsernameExist, "", ValidationSeverity.Error));
        }

        if (entity.Email == request.Email)
        {
            errors.Add(new ValidationError(nameof(request.Email), EmailExist, "", ValidationSeverity.Error));
        }

        return Result.Invalid(errors);
    }

    private User CreateUser(SignUpUserCommand request)
    {
        var id = Ulid.NewUlid().ToGuid();
        var passwordHash = passwordService.GenerateHash(request.Password, out var salt);
        return new User(id, request.Name, request.Email, "user", passwordHash, salt);
    }
}
