using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.UseCases.Cards.GetById;

namespace TodoList.Web.Cards.GetById;

public class GetByIdCard(IMediator mediator) : Endpoint<GetByIdCardRequest, CardRecord>
{
    public override void Configure()
    {
        Get(GetByIdCardRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetByIdCardRequest request, CancellationToken ct)
    {
        var command = new GetByIdCardQuery(request.CardId);

        var result = await mediator.Send(command, ct);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (result.IsSuccess)
        {
            Response = new CardRecord(result.Value.Id, result.Value.Name);
        }
    }
}
