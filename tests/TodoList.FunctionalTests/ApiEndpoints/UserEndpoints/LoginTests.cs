using RestSharp;
using TodoList.Web;
using Xunit;

namespace TodoList.FunctionalTests.ApiEndpoints.UserEndpoints;

public class LoginTests(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly RestClient _client = factory.CreateRestClient();
}
