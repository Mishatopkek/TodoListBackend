using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Card.Details.GetByIdCard;

public record GetByIdCardDetailsQuery(Ulid CardId) : IQuery<Result<CardDetailsDTO>>;
