﻿using Ardalis.Specification;

namespace TodoList.Core.BoardAggregate.Specifications;

public class CreateBoardSpec : Specification<Board>
{
    public CreateBoardSpec(string name, Ulid userId)
    {
        Query.Where(board =>
            board.Name == name &&
            board.UserId == userId.ToGuid()
        );
    }
}
