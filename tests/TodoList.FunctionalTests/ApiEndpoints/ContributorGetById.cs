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
        RestRequest request = new(GetContributorByIdRequest.BuildRoute(1));
        RestResponse<ContributorRecord> response = await _client.ExecuteAsync<ContributorRecord>(request);
        ContributorRecord? result = response.Data;

        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.Name.Should().Be(SeedData.Contributor1.Name);
    }

    [Fact]
    public async Task ReturnsNotFoundGivenId1000()
    {
        RestRequest request = new(GetContributorByIdRequest.BuildRoute(1000));
        RestResponse<ContributorRecord> response = await _client.ExecuteAsync<ContributorRecord>(request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
