using System.Security.Cryptography;
using Bogus;
using FluentAssertions;
using TodoList.Core.Services;
using Xunit;

namespace TodoList.UnitTests.Core.Services;

public class PasswordServiceTests
{
    private readonly Faker _faker = new();
    private readonly PasswordService _sut;
    private readonly string passwordSaltSecret;

    public PasswordServiceTests()
    {
        passwordSaltSecret = _faker.Random.AlphaNumeric(100);
        _sut = new PasswordService(passwordSaltSecret);
    }

    [Fact]
    public void PasswordService_NullOrEmptySalt_ThrowsArgumentException()
    {
        // Act
        Action act1 = () =>
        {
            _ = new PasswordService(null!);
        };
        Action act2 = () =>
        {
            _ = new PasswordService(string.Empty);
        };

        // Assert
        act1.Should().Throw<ArgumentException>()
            .WithMessage("Salt cannot be null or empty*"); // Use wildcard * to match any text after the message
        act2.Should().Throw<ArgumentException>().WithMessage("Salt cannot be null or empty*");
    }

    [Fact]
    public void GenerateHash_ValidPassword_ReturnsHashAndSalt()
    {
        // Arrange
        var password = _faker.Internet.Password();

        // Act
        var result = _sut.GenerateHash(password, out var passwordSalt);

        // Assert
        result.Should().NotBeNullOrEmpty();
        passwordSalt.Should().NotBeEmpty();
        result.Length.Should().Be(64); // SHA-512 produces 64 bytes hash
    }

    [Fact]
    public void GenerateHash_NullOrEmptyPassword_ThrowsArgumentException()
    {
        // Act
        Action act1 = () => _sut.GenerateHash(null!, out _);
        Action act2 = () => _sut.GenerateHash(string.Empty, out _);

        // Assert
        act1.Should().Throw<ArgumentException>()
            .WithMessage("Password cannot be null or empty*"); // Use wildcard * to match any text after the message
        act2.Should().Throw<ArgumentException>().WithMessage("Password cannot be null or empty*");
    }

    [Fact]
    public void Hash_ValidPasswordAndSalt_ReturnsHash()
    {
        // Arrange
        var password = _faker.Internet.Password();
        var salt = new HMACSHA512().Key;

        // Act
        var result = _sut.Hash(password, salt);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(64);
    }

    [Fact]
    public void Hash_NullOrEmptyPassword_ThrowsArgumentException()
    {
        // Arrange
        var salt = new HMACSHA512().Key;

        // Act
        Action act1 = () => _sut.Hash(null!, salt);
        Action act2 = () => _sut.Hash(string.Empty, salt);

        // Assert
        act1.Should().Throw<ArgumentException>().WithMessage("Password cannot be null or empty*");
        act2.Should().Throw<ArgumentException>().WithMessage("Password cannot be null or empty*");
    }

    [Fact]
    public void Hash_NullOrEmptySalt_ThrowsArgumentException()
    {
        // Arrange
        var password = _faker.Internet.Password();

        // Act
        Action act1 = () => _sut.Hash(password, null!);
        Action act2 = () => _sut.Hash(password, []);

        // Assert
        act1.Should().Throw<ArgumentException>().WithMessage("Salt cannot be null or empty*");
        act2.Should().Throw<ArgumentException>().WithMessage("Salt cannot be null or empty*");
    }

    [Fact]
    public void ArePasswordsEqual_SameHashes_ReturnsTrue()
    {
        // Arrange
        var password = _faker.Internet.Password();
        var salt = new HMACSHA512().Key;
        var hashedPassword = _sut.Hash(password, salt);

        // Act
        var result = _sut.ArePasswordsEqual(hashedPassword, hashedPassword);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void ArePasswordsEqual_DifferentHashes_ReturnsFalse()
    {
        // Arrange
        var password1 = _faker.Internet.Password();
        var password2 = _faker.Internet.Password();
        var salt = new HMACSHA512().Key;
        var hashedPassword1 = _sut.Hash(password1, salt);
        var hashedPassword2 = _sut.Hash(password2, salt);

        // Act
        var result = _sut.ArePasswordsEqual(hashedPassword1, hashedPassword2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ArePasswordsEqual_DifferentHashesSize_ReturnsFalse()
    {
        // Arrange
        var wrongPassword1 = _faker.Random.Bytes(128);
        var wrongPassword2 = _faker.Random.Bytes(64);

        // Act
        var result = _sut.ArePasswordsEqual(wrongPassword1, wrongPassword2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ArePasswordsEqual_NullOrEmptyHashedPassword_ThrowsArgumentException()
    {
        // Arrange
        var randomHash = _faker.Random.Bytes(128);

        // Act
        Action act1 = () => _sut.ArePasswordsEqual(null!, null!);
        Action act2 = () => _sut.ArePasswordsEqual(randomHash, null!);
        Action act3 = () => _sut.ArePasswordsEqual(null!, randomHash);

        // Assert
        act1.Should().Throw<ArgumentException>().WithMessage("Passwords cannot be null");
        act2.Should().Throw<ArgumentException>().WithMessage("Passwords cannot be null");
        act3.Should().Throw<ArgumentException>().WithMessage("Passwords cannot be null");
    }
}
