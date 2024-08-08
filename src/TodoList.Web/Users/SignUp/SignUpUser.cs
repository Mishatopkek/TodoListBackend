using FastEndpoints;
using MediatR;
using TodoList.UseCases.Users.SignUp;

namespace TodoList.Web.Users.SignUp;

public class SignUpUser(IMediator mediator) : Endpoint<SignUpUserRequest, SignUpUserResponse>
{
    public override void Configure()
    {
        Post(SignUpUserRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            var simpleUserRequest = new SignUpUserRequest
            {
                Name = "superuniquename", Email = "supername@gmail.com", Password = "qwer1234asdf"
            };
            s.RequestExamples.Add(new RequestExample(simpleUserRequest, "User example"));

            var anotherSimpleUserRequest =
                new SignUpUserRequest {Name = "Mishatopkek", Email = "Misha@top.kek", Password = "qwer1234!@#$"};
            s.RequestExamples.Add(new RequestExample(anotherSimpleUserRequest, "Another user example"));
        });
    }

    public override async Task HandleAsync(SignUpUserRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(new SignUpUserCommand(req.Name, req.Email, req.Password), ct);

        if (result.IsSuccess)
        {
            Response = new SignUpUserResponse(result.Value);
        }
    }
}
