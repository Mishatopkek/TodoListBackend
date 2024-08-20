using System.Security.Cryptography;
using System.Text;
using TodoList.Core.Interfaces;

namespace TodoList.Core.Services;

public class PasswordService(string constantSalt) : IPasswordService
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
}
