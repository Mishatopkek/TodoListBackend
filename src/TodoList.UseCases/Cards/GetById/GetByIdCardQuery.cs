using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Cards.GetById;

public record GetByIdCardQuery(Ulid CardId) : IQuery<Result<CardDto>>;
