using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Cards.GetById;

public record GetByIdCardQuery(int CardId) : IQuery<Result<CardDto>>;
