using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Ardalis.Specification;
using Microsoft.IdentityModel.Tokens;
using TodoList.Core.UserAggregate;
using TodoList.Core.UserAggregate.Specifications;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TodoList.UseCases.Users.Login;

public class LoginUserHandler(IRepositoryBase<User> repository) : IQueryHandler<LoginUserQuery, Result<UserDTO>>
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
        var enteredHashedPassword = HashPassword(saltedRequestPassword, entity.PasswordSalt);

        var arePasswordsEqual = ArePasswordsEqual(enteredHashedPassword, entity.Password);

        if (!arePasswordsEqual)
        {
            return Result.NotFound();
        }

        var token = GenerateJwtToken(entity.Id, entity.Name, entity.Email, entity.Role);

        return new UserDTO(token);
    }

    // Function to generate a random salt
    private static byte[] GenerateSalt()
    {
        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[16]; // 128-bit salt
        rng.GetBytes(salt);
        return salt;
    }

    // Function to hash a password with a given salt using HMAC-SHA512
    private static byte[] HashPassword(string password, byte[] salt)
    {
        using var hmac = new HMACSHA512(salt);
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    // Function to compare two byte arrays for equality
    private static bool ArePasswordsEqual(byte[] hashedPassword1, byte[] hashedPassword2)
    {
        if (hashedPassword1.Length != hashedPassword2.Length)
        {
            return false;
        }

        return !hashedPassword1.Where((t, i) => t != hashedPassword2[i]).Any();
    }

    public static string GenerateJwtToken(Guid userId, string username, string email, string role)
    {
        // Define the security key
        var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET");
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new ArgumentNullException(nameof(secretKey), "Jwt secret is not initialized");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Define the claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Email, email), new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Create the JWT token
        var token = new JwtSecurityToken(
            "https://todo.mishahub.com",
            "https://todo.mishahub.com/api",
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
