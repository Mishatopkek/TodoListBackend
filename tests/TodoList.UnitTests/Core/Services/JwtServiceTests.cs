using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bogus;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using TodoList.Core.Services;
using TodoList.Core.UserAggregate;
using Xunit;

namespace TodoList.UnitTests.Core.Services;

public class JwtServiceTests
{
    private readonly string _audience;
    private readonly Faker _faker;
    private readonly string _issuer;
    private readonly string _jwtSecret;
    private readonly JwtService _jwtService;

    public JwtServiceTests()
    {
        _faker = new Faker();
        _jwtSecret = _faker.Random.AlphaNumeric(128); // Generate a random JWT secret for tests
        _issuer = _faker.Internet.Url();
        _audience = _faker.Internet.Url();
        _jwtService = new JwtService(_jwtSecret, _issuer, _audience);
    }

    [Fact]
    public void GenerateJwtToken_ValidUser_ReturnsToken()
    {
        // Arrange
        var user = new User(
            Ulid.NewUlid().ToGuid(),
            _faker.Person.UserName,
            _faker.Person.Email,
            "Admin",
            _faker.Random.Bytes(128),
            _faker.Random.Bytes(128));

        // Act
        var token = _jwtService.GenerateJwtToken(user);

        // Assert
        token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GenerateJwtToken_ValidParameters_ReturnsToken()
    {
        // Arrange
        var userId = Ulid.NewUlid().ToGuid();
        var username = _faker.Person.UserName;
        var email = _faker.Person.Email;
        const string role = "User";

        // Act
        var token = _jwtService.GenerateJwtToken(userId, username, email, role);

        // Assert
        token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ValidateJwtToken_ValidToken_ReturnsClaimsPrincipal()
    {
        // Arrange
        var userId = Ulid.NewUlid().ToGuid();
        var username = _faker.Person.UserName;
        var email = _faker.Person.Email;
        const string role = "User";
        var token = _jwtService.GenerateJwtToken(userId, username, email, role);

        // Act
        ClaimsPrincipal claimsPrincipal = _jwtService.ValidateJwtToken(token);

        // Assert
        claimsPrincipal.Should().NotBeNull();
        claimsPrincipal.Identity.Should().NotBeNull();
        claimsPrincipal.Identity!.Name.Should().Be(username);
        claimsPrincipal.HasClaim(c =>
            c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" &&
            c.Value == userId.ToString()).Should().BeTrue();
        claimsPrincipal.HasClaim(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress" && c.Value == email)
            .Should()
            .BeTrue();
        claimsPrincipal.HasClaim(c => c is {Type: ClaimTypes.Role, Value: role}).Should().BeTrue();
    }

    [Fact]
    public void ValidateJwtToken_InvalidToken_ThrowsSecurityTokenException()
    {
        // Arrange
        const string invalidToken = "invalid.token.here";

        // Act
        Action act = () => _jwtService.ValidateJwtToken(invalidToken);

        // Assert
        act.Should().Throw<SecurityTokenException>().WithMessage("Token validation failed");
    }

    [Fact]
    public void ValidateJwtToken_ExpiredToken_ThrowsSecurityTokenException()
    {
        // Arrange
        var userId = Ulid.NewUlid().ToGuid();
        var username = _faker.Person.UserName;
        var email = _faker.Person.Email;
        const string role = "User";

        // Generate token with immediate expiration for testing
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(JwtRegisteredClaimNames.Email, email), new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            },
            expires: DateTime.UtcNow.AddSeconds(-1), // Expired token
            signingCredentials: credentials
        );

        var expiredToken = new JwtSecurityTokenHandler().WriteToken(token);

        // Act
        Action act = () => _jwtService.ValidateJwtToken(expiredToken);

        // Assert
        act.Should().Throw<SecurityTokenException>().WithMessage("Token validation failed");
    }

    [Fact(Skip = "None is not possible for tests")]
    public void ValidateJwtToken_InvalidTokenType_ThrowsSecurityTokenException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var username = _faker.Person.UserName;
        var email = _faker.Person.Email;
        const string role = "User";
        var falseJwtSecret = _faker.Random.AlphaNumeric(128);

        // Generate a token with an invalid security key (to make it invalid for JwtSecurityToken)
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(falseJwtSecret));

        // Incorrectly use an empty string for the algorithm, making the token invalid
        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(JwtRegisteredClaimNames.Email, email), new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            },
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.None)
        );

        var invalidTokenType = new JwtSecurityTokenHandler().WriteToken(token);

        // Act
        Action act = () => _jwtService.ValidateJwtToken(invalidTokenType);

        // Assert
        act.Should().Throw<SecurityTokenException>().WithMessage("Invalid token");
    }

    [Fact]
    public void ValidateJwtToken_InvalidAlgorithm_ThrowsSecurityTokenException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var username = _faker.Person.UserName;
        var email = _faker.Person.Email;
        const string role = "User";

        // Generate a token with a different algorithm (e.g., HmacSha512 instead of HmacSha256)
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(JwtRegisteredClaimNames.Email, email), new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            },
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        var invalidAlgorithmToken = new JwtSecurityTokenHandler().WriteToken(token);

        // Act
        Action act = () => _jwtService.ValidateJwtToken(invalidAlgorithmToken);

        // Assert
        act.Should().Throw<SecurityTokenException>().WithMessage("Invalid token");
    }
}
