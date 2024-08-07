﻿using Ardalis.Specification;
using TodoList.Core.Extentions;

namespace TodoList.Core.CardAggregate.Specifications;

public sealed class CardByIdSpec : Specification<Card>
{
    public CardByIdSpec(Ulid cardId)
    {
        Query.Where(card => card.Id.ToUlid() == cardId);
    }
}
