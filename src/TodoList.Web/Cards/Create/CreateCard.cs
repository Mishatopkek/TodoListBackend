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
            var simpleCard = new CreateCardRequest {Name = "My new card"};
            s.RequestExamples.Add(new RequestExample(simpleCard, "Simple card"));
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
