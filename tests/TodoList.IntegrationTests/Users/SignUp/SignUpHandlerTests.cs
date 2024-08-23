using Ardalis.Result;
using Ardalis.SharedKernel;
using Bogus;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TodoList.Core.Interfaces;
using TodoList.Core.UserAggregate;
using TodoList.UseCases.Users.SignUp;
using Xunit;

namespace TodoList.IntegrationTests.Users.SignUp;

public class SignUpHandlerTests
{
    [Fact]
    public async Task Handle_RandomServiceShouldThrow_ReturnError()
    {
        // Arrange
        IRepository<User> repository = Substitute.For<IRepository<User>>();
        IPasswordService passwordService = Substitute.For<IPasswordService>();
        IJwtService jwtService = Substitute.For<IJwtService>();
        jwtService.GenerateJwtToken(null!).ThrowsForAnyArgs(new Exception());
        Faker faker = new();
        var username = faker.Person.UserName;
        var email = faker.Person.Email;
        var password = faker.Internet.Password();
        CancellationToken cancellationToken = CancellationToken.None;
        var command = new SignUpUserCommand(username, email, password);
        var handler = new SignUpUserHandler(repository, passwordService, jwtService);

        // Act
        Result<string> result = await handler.Handle(command, cancellationToken);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Status.Should().Be(ResultStatus.Error);
    }

    [Fact]
    public async Task Handle_ShouldNotThrow_ReturnError()
    {
        // Arrange
        IRepository<User> repository = Substitute.For<IRepository<User>>();
        IPasswordService passwordService = Substitute.For<IPasswordService>();
        IJwtService jwtService = Substitute.For<IJwtService>();
        Faker faker = new();
        var username = faker.Person.UserName;
        var email = faker.Person.Email;
        var password = faker.Internet.Password();
        CancellationToken cancellationToken = CancellationToken.None;
        var command = new SignUpUserCommand(username, email, password);
        var handler = new SignUpUserHandler(repository, passwordService, jwtService);

        // Act
        Func<Task<Result<string>>> act = () => handler.Handle(command, cancellationToken);

        // Assert
        await act.Should().NotThrowAsync();
    }
}
