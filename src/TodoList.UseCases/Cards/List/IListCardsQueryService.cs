namespace TodoList.UseCases.Cards.List;

public interface IListCardsQueryService
{
    Task<IEnumerable<CardDto>> ListAsync();
}
