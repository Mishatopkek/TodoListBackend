using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoList.Core.Interfaces;
using TodoList.Core.UserAggregate;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TodoList.Core.Services;

public class PasswordService(string constantSalt, string jwtSecret) : IPasswordService
{
    // Function to hash a password with a given salt using HMAC-SHA512
    public byte[] GenerateHash(string password, out byte[] passwordSalt)
    {
        using HMACSHA512 hmac = new();

        var passwordHashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password + constantSalt));

        passwordSalt = hmac.Key;

        return passwordHashBytes;
    }

    public byte[] Hash(string password, byte[] salt)
    {
        using var hmac = new HMACSHA512(salt);
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(password + constantSalt));
    }

    // Function to compare two byte arrays for equality
    public bool ArePasswordsEqual(byte[] hashedPassword1, byte[] hashedPassword2)
    {
        if (hashedPassword1.Length != hashedPassword2.Length)
        {
            return false;
        }

        return !hashedPassword1.Where((t, i) => t != hashedPassword2[i]).Any();
    }

    public string GenerateJwtToken(User user)
    {
        var token = GenerateJwtToken(user.Id, user.Name, user.Email, user.Role);
        return token;
    }

    public string GenerateJwtToken(Guid userId, string username, string email, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
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
