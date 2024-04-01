using TodoList.Core.ContributorAggregate;
using Xunit;

namespace TodoList.UnitTests.Core.ContributorAggregate;

public class ContributorConstructor
{
    private readonly string _testName = "test name";
    private Contributor? _testContributor;

    private Contributor CreateContributor()
    {
        return new Contributor(_testName);
    }

    [Fact]
    public void InitializesName()
    {
        _testContributor = CreateContributor();

        Assert.Equal(_testName, _testContributor.Name);
    }
}
