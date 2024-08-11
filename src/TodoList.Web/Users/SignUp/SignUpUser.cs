using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.Core.Extensions;
using TodoList.UseCases.Users.SignUp;

namespace TodoList.Web.Users.SignUp;

public class SignUpUser(IMediator mediator) : Endpoint<SignUpUserRequest>
{
    public override void Configure()
    {
        Post(SignUpUserRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            RequestExamples(s);

            ResponseExamples(s);
        });
    }

    public override async Task HandleAsync(SignUpUserRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(new SignUpUserCommand(req.Name, req.Email, req.Password), ct);

        switch (result.Status)
        {
            case ResultStatus.Created:
                await SendAsync(new SignUpUserResponseCreated(result.Value), StatusCodes.Status201Created, ct);
                break;
            case ResultStatus.Invalid:
                Console.WriteLine(result);
                await SendAsync(result.ToErrorResponse(), StatusCodes.Status409Conflict, ct);
                break;
        }
    }

    private static void RequestExamples(EndpointSummary s)
    {
        var simpleUserRequest = new SignUpUserRequest
        {
            Name = "superuniquename", Email = "supername@gmail.com", Password = "qwer1234asdf"
        };
        s.RequestExamples.Add(new RequestExample(simpleUserRequest, "User example"));

        var anotherSimpleUserRequest =
            new SignUpUserRequest {Name = "Mishatopkek", Email = "Misha@top.kek", Password = "qwer1234!@#$"};
        s.RequestExamples.Add(new RequestExample(anotherSimpleUserRequest, "Another user example"));
    }

    private static void ResponseExamples(EndpointSummary s)
    {
        s.Response(StatusCodes.Status201Created, "Get user's jwt token",
            example: new SignUpUserResponseCreated(
                "qwehbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6Ik1pc2hhIiwicm9sZSI6InVzZXIiLCJpYXQiOjE3MjI1NDc4MDAsImV4cCI6MTczMjU1MTQwMH0.lcINuF4acYXJMH2ubDpRX18LsrF5M5JOGiTLPhdVZVQ"));
        s.Response(StatusCodes.Status409Conflict, "Error with given data",
            example: new ErrorResponse
            {
                StatusCode = 409,
                Errors = new Dictionary<string, List<string>>
                {
                    ["name"] = ["Username is already exist"],
                    ["email"] = ["Email is already exist"],
                    ["password"] = ["Password is already exist xD"]
                }
            });
    }
}
