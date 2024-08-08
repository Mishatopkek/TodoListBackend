using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.UseCases.Users.Login;

namespace TodoList.Web.Users.Login;

public class LoginUser(IMediator mediator) : Endpoint<LoginUserRequest, LoginUserResponse>
{
    public override void Configure()
    {
        Post(LoginUserRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            var usernameExample = new LoginUserRequest {Login = "superuniquename", Password = "qwer1234asdf"};
            s.RequestExamples.Add(new RequestExample(usernameExample, "User example"));

            var emailExample = new LoginUserRequest {Login = "supername@gmail.com", Password = "qwer1234!@#$"};
            s.RequestExamples.Add(new RequestExample(emailExample, "Another user example"));
        });
    }

    public override async Task HandleAsync(LoginUserRequest req, CancellationToken ct)
    {
        var query = new LoginUserQuery(req.Login, req.Password);
        var result = await mediator.Send(query, ct);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (result.IsSuccess)
        {
            Response = new LoginUserResponse(result.Value.Token);
        }
    }
}
