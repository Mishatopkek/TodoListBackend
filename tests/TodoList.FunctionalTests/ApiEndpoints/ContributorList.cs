using FluentAssertions;
using RestSharp;
using TodoList.Infrastructure.Data;
using TodoList.Web;
using TodoList.Web.Contributors;
using Xunit;

namespace TodoList.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class ContributorList(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly RestClient _client = factory.CreateRestClient();

    [Fact]
    public async Task ReturnsTwoContributors()
    {
        ContributorListResponse? result = await _client.GetAsync<ContributorListResponse>("/Contributors");

        result!.Contributors.Should().HaveCount(2);
        result.Contributors.Should().Contain(i => i.Name == SeedData.Contributor1.Name);
        result.Contributors.Should().Contain(i => i.Name == SeedData.Contributor2.Name);
    }
}
