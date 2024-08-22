using TodoList.Core.ContributorAggregate;
using TodoList.Infrastructure.Data;
using Xunit;

namespace TodoList.IntegrationTests.Data;

public class EfRepositoryDelete : BaseEfRepoTestFixture
{
    [Fact]
    public async Task DeletesItemAfterAddingIt()
    {
        // add a Contributor
        EfRepository<Contributor> repository = GetRepository();
        var initialName = Guid.NewGuid().ToString();
        Contributor contributor = new(initialName);
        await repository.AddAsync(contributor);

        // delete the item
        await repository.DeleteAsync(contributor);

        // verify it's no longer there
        Assert.DoesNotContain(await repository.ListAsync(), prevContributor => prevContributor.Name == initialName);
    }
}
