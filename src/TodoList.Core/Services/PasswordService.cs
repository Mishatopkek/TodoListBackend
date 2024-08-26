using System.Security.Cryptography;
using System.Text;
using TodoList.Core.Interfaces;

namespace TodoList.Core.Services;

public class PasswordService : IPasswordService
{
    private readonly string _constantSalt;

    public PasswordService(string constantSalt)
    {
        if (string.IsNullOrWhiteSpace(constantSalt))
        {
            throw new ArgumentException("Salt cannot be null or empty", nameof(constantSalt));
        }

        _constantSalt = constantSalt;
    }

    // Function to hash a password with a given salt using HMAC-SHA512
    public byte[] GenerateHash(string password, out byte[] passwordSalt)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password cannot be null or empty", nameof(password));
        }

        using HMACSHA512 hmac = new();

        var passwordHashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password + _constantSalt));

        passwordSalt = hmac.Key;

        return passwordHashBytes;
    }

    public byte[] Hash(string password, byte[] salt)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password cannot be null or empty", nameof(password));
        }

        if (salt == null || salt.Length == 0)
        {
            throw new ArgumentException("Salt cannot be null or empty", nameof(salt));
        }

        using var hmac = new HMACSHA512(salt);
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(password + _constantSalt));
    }

    // Function to compare two byte arrays for equality
    public bool ArePasswordsEqual(byte[] hashedPassword1, byte[] hashedPassword2)
    {
        if (hashedPassword1 == null || hashedPassword2 == null)
        {
            throw new ArgumentException("Passwords cannot be null");
        }

        if (hashedPassword1.Length != hashedPassword2.Length)
        {
            return false;
        }

        return !hashedPassword1.Where((t, i) => t != hashedPassword2[i]).Any();
    }
}
