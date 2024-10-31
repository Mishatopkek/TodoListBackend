using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Card.Details.GetByIdCard;

public class GetByIdCardDetailsHandler(IGetByIdCardDetailsService service)
    : IQueryHandler<GetByIdCardDetailsQuery, Result<CardDetailsDTO>>
{
    public async Task<Result<CardDetailsDTO>> Handle(GetByIdCardDetailsQuery request,
        CancellationToken cancellationToken)
    {
        CardDetailsDTO a = await service.GetCardDetailsAsync(request.CardId);

        return Result.Success(a);
    }
}
