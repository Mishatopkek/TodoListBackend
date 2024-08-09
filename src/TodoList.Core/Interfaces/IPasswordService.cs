using TodoList.Core.UserAggregate;

namespace TodoList.Core.Interfaces;

public interface IPasswordService
{
    byte[] HashPassword(string password, out byte[] passwordSalt);
    bool ArePasswordsEqual(byte[] hashedPassword1, byte[] hashedPassword2);
    string GenerateJwtToken(User user);
    string GenerateJwtToken(Guid userId, string username, string email, string role);
}
