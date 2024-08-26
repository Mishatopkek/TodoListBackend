using System.Security.Claims;
using TodoList.Core.UserAggregate;

namespace TodoList.Core.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(User user);
    string GenerateJwtToken(Guid userId, string username, string email, string role);
    ClaimsPrincipal ValidateJwtToken(string token);
}
