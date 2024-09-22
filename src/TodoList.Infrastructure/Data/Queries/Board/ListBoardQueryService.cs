﻿using Microsoft.EntityFrameworkCore;
using TodoList.Core.Extensions;
using TodoList.UseCases.Boards;
using TodoList.UseCases.Boards.List;

namespace TodoList.Infrastructure.Data.Queries.Board;

public class ListBoardQueryService(AppDbContext db) : IListBoardQueryService
{
    public async Task<IEnumerable<BoardDTO>> ListAsync()
    {
        List<BoardDTO> result = await db.Boards
            .Include(x => x.User)
            .Select(b => new BoardDTO(b.Id.ToUlid(), b.Name, b.Title, b.User.Name, new List<ColumnDTO>()))
            .ToListAsync();

        return result;
    }
}
