using FastEndpoints;
using MediatR;
using TodoList.UseCases.Cards.Create;

namespace TodoList.Web.Cards.Create;

public class CreateCard(IMediator mediator) : Endpoint<CreateCardRequest, CreateCardResponse>
{
    public override void Configure()
    {
        Post(CreateCardRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            var withDataExample = new CreateCardRequest {Name = "My new card"};
            s.RequestExamples.Add(new RequestExample(new CreateCardRequest(), "Empty request"));
            s.RequestExamples.Add(new RequestExample(withDataExample, "With data request"));
        });
    }

    public override async Task HandleAsync(
        CreateCardRequest request,
        CancellationToken ct)
    {
        var result = await mediator.Send(new CreateCardCommand(request.Name!), ct);

        if (result.IsSuccess)
        {
            Response = new CreateCardResponse(result.Value, request.Name!);
        }
    }
}
