﻿using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace TodoList.Core.BoardAggregate;

public class Column : EntityBase<Guid>, IAggregateRoot
{
    public Column()
    {
    }

    public Column(Guid boardId, Board board, string title, bool showAddCardByDefault, List<Card> cards)
    {
        BoardId = Guard.Against.Null(boardId, nameof(boardId));
        Board = Guard.Against.Null(board, nameof(board));
        Title = Guard.Against.NullOrEmpty(title, nameof(title));
        ShowAddCardByDefault = Guard.Against.Null(showAddCardByDefault, nameof(showAddCardByDefault));
        Cards = Guard.Against.Null(cards, nameof(cards));
    }

    public Guid BoardId { get; set; } = Guid.Empty;
    public Board Board { get; set; } = null!;

    public string Title { get; set; } = null!;
    public bool ShowAddCardByDefault { get; set; }
    public List<Card> Cards { get; set; } = null!;
}
