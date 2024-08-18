using System.Net;
using FluentAssertions;
using RestSharp;
using TodoList.Infrastructure.Data;
using TodoList.Web;
using TodoList.Web.Contributors;
using Xunit;

namespace TodoList.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class ContributorGetById(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly RestClient _client = factory.CreateRestClient();

    [Fact]
    public async Task ReturnsSeedContributorGivenId1()
    {
        ContributorRecord? result = await _client.GetAsync<ContributorRecord>(GetContributorByIdRequest.BuildRoute(1));

        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.Name.Should().Be(SeedData.Contributor1.Name);
    }

    [Fact]
    public async Task ReturnsNotFoundGivenId1000()
    {
        RestResponse result = await _client.ExecuteGetAsync(GetContributorByIdRequest.BuildRoute(1000));

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
