using Ardalis.Result;
using Ardalis.SharedKernel;
using Ardalis.Specification;
using TodoList.Core.CardAggregate;
using TodoList.Core.CardAggregate.Specifications;
using TodoList.Core.Extentions;

namespace TodoList.UseCases.Cards.GetById;

public class GetByIdCardHandler(IReadRepositoryBase<Card> repository) : IQueryHandler<GetByIdCardQuery, Result<CardDto>>
{
    public async Task<Result<CardDto>> Handle(GetByIdCardQuery request, CancellationToken cancellationToken)
    {
        var spec = new CardByIdSpec(request.CardId);
        var entity = await repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (entity == null)
        {
            return Result.NotFound();
        }

        return new CardDto(entity.Id.ToUlid(), entity.Name);
    }
}
