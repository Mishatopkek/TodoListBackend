using Ardalis.SharedKernel;
using FluentAssertions;
using NSubstitute;
using TodoList.Core.ContributorAggregate;
using TodoList.UseCases.Contributors.Create;
using Xunit;

namespace TodoList.UnitTests.UseCases.Contributors;

public class CreateContributorHandlerHandle
{
    private readonly IRepository<Contributor> _repository = Substitute.For<IRepository<Contributor>>();
    private readonly string _testName = "test name";
    private readonly CreateContributorHandler _handler;

    public CreateContributorHandlerHandle()
    {
        _handler = new CreateContributorHandler(_repository);
    }

    private Contributor CreateContributor()
    {
        return new Contributor(_testName);
    }

    [Fact]
    public async Task ReturnsSuccessGivenValidName()
    {
        _repository.AddAsync(Arg.Any<Contributor>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(CreateContributor()));
        var result = await _handler.Handle(new CreateContributorCommand(_testName), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
    }
}
