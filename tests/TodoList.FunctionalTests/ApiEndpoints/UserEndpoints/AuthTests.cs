using System.Net;
using System.Security.Claims;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using TodoList.Core.Interfaces;
using TodoList.Web;
using TodoList.Web.Users.Login;
using TodoList.Web.Users.SignUp;
using Xunit;

namespace TodoList.FunctionalTests.ApiEndpoints.UserEndpoints;

public class AuthTests(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly RestClient _client = factory.CreateRestClient();

    [Fact]
    public async Task FullAuthFlow()
    {
        await using AsyncServiceScope scope = factory.Services.CreateAsyncScope();
        IJwtService jwtService = scope.ServiceProvider.GetRequiredService<IJwtService>();
        Faker faker = new();
        var name = faker.Internet.UserName().Replace(".", "");
        var email = faker.Internet.Email();
        var password = faker.Internet.Password();
        LoginUserRequest loginUserRequest = new() {Login = name, Password = password};
        SignUpUserRequest signUpUserRequest = new() {Name = name, Email = email, Password = password};

        await CheckLoginNotFound(loginUserRequest);

        await CheckSignUpCreated(signUpUserRequest, jwtService);

        await CheckLoginOk(loginUserRequest, jwtService);

        await CheckSignUpConflict(signUpUserRequest);
    }

    private async Task CheckLoginNotFound(LoginUserRequest loginUserRequest)
    {
        RestResponse<LoginUserResponse> loginNotFound = await Login(loginUserRequest);
        loginNotFound.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private async Task<RestResponse<LoginUserResponse>> Login(LoginUserRequest loginUserRequest)
    {
        RestRequest request = new(LoginUserRequest.Route, Method.Post);
        request.AddJsonBody(loginUserRequest);
        RestResponse<LoginUserResponse> response = await _client.ExecuteAsync<LoginUserResponse>(request);
        return response;
    }

    private async Task CheckSignUpCreated(SignUpUserRequest requestModel, IJwtService jwtService)
    {
        RestResponse<SignUpUserResponseCreated> response = await SignUp(requestModel);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        SignUpUserResponseCreated? result = response.Data;

        result.Should().NotBeNull();

        var token = result!.Token;
        token.Should().NotBeNullOrEmpty();

        jwtService.ValidateJwtToken(token);
    }

    private async Task<RestResponse<SignUpUserResponseCreated>> SignUp(SignUpUserRequest requestModel)
    {
        RestRequest request = new(SignUpUserRequest.Route, Method.Post);
        request.AddJsonBody(requestModel);
        RestResponse<SignUpUserResponseCreated> response =
            await _client.ExecuteAsync<SignUpUserResponseCreated>(request);
        return response;
    }

    private async Task CheckLoginOk(LoginUserRequest loginUserRequest, IJwtService jwtService)
    {
        RestResponse<LoginUserResponse> loginCreate = await Login(loginUserRequest);
        ValidateLogin(loginCreate, jwtService);
    }

    private static void ValidateLogin(RestResponse<LoginUserResponse> loginCreate, IJwtService jwtService)
    {
        loginCreate.StatusCode.Should().Be(HttpStatusCode.OK);

        LoginUserResponse? data = loginCreate.Data;
        data.Should().NotBeNull();

        var token = data!.Token;
        token.Should().NotBeNullOrEmpty();

        ClaimsPrincipal loginClaims = jwtService.ValidateJwtToken(token);
        loginClaims.Should().NotBeNull();
    }

    private async Task CheckSignUpConflict(SignUpUserRequest signUpUserRequest)
    {
        RestResponse<SignUpUserResponseCreated> response = await SignUp(signUpUserRequest);
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
}
