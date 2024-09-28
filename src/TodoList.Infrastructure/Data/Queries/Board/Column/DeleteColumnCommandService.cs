using Microsoft.EntityFrameworkCore;
using TodoList.UseCases.Boards.Column.Delete;

namespace TodoList.Infrastructure.Data.Queries.Board.Column;

public class DeleteColumnCommandService(AppDbContext db) : IDeleteColumnService
{
    public async Task DeleteAsync(Ulid columnId, Ulid userId)
    {
        //TODO: add check if user can delete the column
        Core.BoardAggregate.Column column = await db
            .Columns
            .FirstAsync(c => c.Id == columnId.ToGuid());

        db.Columns.Remove(column);
        await db.SaveChangesAsync();
    }
}
