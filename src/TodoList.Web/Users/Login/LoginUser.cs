using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.UseCases.Users.Login;
using TodoList.Web.Cards.Create;

namespace TodoList.Web.Users.Login;

public class LoginUser(IMediator mediator) : Endpoint<LoginUserRequest, LoginUserResponse>
{
    public override void Configure()
    {
        Post(LoginUserRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            var simpleCard = new CreateCardRequest {Name = "My new card"};
            s.RequestExamples.Add(new RequestExample(simpleCard, "Simple card"));
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
