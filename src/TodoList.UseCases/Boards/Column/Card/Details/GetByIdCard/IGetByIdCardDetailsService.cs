namespace TodoList.UseCases.Boards.Column.Card.Details.GetByIdCard;

public interface IGetByIdCardDetailsService
{
    Task<CardDetailsDTO> GetCardDetailsAsync(Ulid id);
}
