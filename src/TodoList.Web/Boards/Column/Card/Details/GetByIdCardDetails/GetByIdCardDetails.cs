using Ardalis.Result;
using FastEndpoints;
using MediatR;
using TodoList.UseCases.Boards;
using TodoList.UseCases.Boards.Column.Card.Details.GetByIdCard;

namespace TodoList.Web.Boards.Column.Card.Details.GetByIdCardDetails;

public class GetByIdCardDetails(IMediator mediator) : Endpoint<GetByIdCardDetailsRequest, GetByIdCardDetailsResponse>
{
    public override void Configure()
    {
        Get(GetByIdCardDetailsRequest.Route);
    }

    public override async Task HandleAsync(GetByIdCardDetailsRequest req, CancellationToken ct)
    {
        Result<CardDetailsDTO> response = await mediator.Send(new GetByIdCardDetailsQuery(req.Id), ct);

        if (response.IsSuccess)
        {
            CardDetailsDTO? value = response.Value;
            await SendOkAsync(
                new GetByIdCardDetailsResponse
                {
                    Description = value.Description,
                    Comments = value.Comments.Select(comment => new GetByIdCardDetailsResponseComment
                    {
                        Id = comment.Id, Text = comment.Text, UserId = comment.UserId
                    }),
                    Users = []
                }, ct);
        }
    }
}
