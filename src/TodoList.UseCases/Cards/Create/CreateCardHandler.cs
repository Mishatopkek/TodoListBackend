using Ardalis.Result;
using Ardalis.SharedKernel;
using Ardalis.Specification;
using TodoList.Core.CardAggregate;
using TodoList.Core.Extentions;

namespace TodoList.UseCases.Cards.Create;

public class CreateCardHandler(IRepositoryBase<Card> repository)
    : ICommandHandler<CreateCardCommand, Result<Ulid>>
{
    public async Task<Result<Ulid>> Handle(CreateCardCommand request,
        CancellationToken cancellationToken)
    {
        var newCard = new Card(request.Name);
        var createdCard = await repository.AddAsync(newCard, cancellationToken);

        return createdCard.Id.ToUlid();
    }
}
