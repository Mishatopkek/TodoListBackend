using System.Security.Claims;
using TodoList.Core.UserAggregate;

namespace TodoList.Core.Extensions;

public static class ClaimsPrincipalExtensions
{
    /// <summary>
    ///     Extracts the user information from the claims of the authenticated user (JWT token).
    ///     Throws an exception if any required claim (UserId, Username, Email, Role) is missing or invalid.
    /// </summary>
    /// <param name="user">The ClaimsPrincipal object representing the authenticated user.</param>
    /// <returns>A JwtUserModel containing the extracted claims.</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when the user is not authenticated.</exception>
    /// <exception cref="ArgumentException">
    ///     Thrown when any required claim (UserId, Username, Email, Role) is missing or
    ///     invalid.
    /// </exception>
    public static JwtUserModel GetJwtUser(this ClaimsPrincipal user)
    {
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userName = user.FindFirst(ClaimTypes.Name)?.Value;
        var email = user.FindFirst(ClaimTypes.Email)?.Value;
        var role = user.FindFirst(ClaimTypes.Role)?.Value;

        // Check if any of the required claims are null or empty
        if (string.IsNullOrEmpty(userId))
        {
            throw new ArgumentException("UserId is missing or invalid in the token.");
        }

        if (string.IsNullOrEmpty(userName))
        {
            throw new ArgumentException("UserName is missing or invalid in the token.");
        }

        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Email is missing or invalid in the token.");
        }

        if (string.IsNullOrEmpty(role))
        {
            throw new ArgumentException("Role is missing or invalid in the token.");
        }

        return new JwtUserModel {UserId = userId.ToUlid(), Username = userName, Email = email, Role = role};
    }
}
