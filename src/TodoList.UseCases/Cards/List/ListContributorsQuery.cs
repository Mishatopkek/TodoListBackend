using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Cards.List;

public record ListCardsQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<CardDto>>>;
