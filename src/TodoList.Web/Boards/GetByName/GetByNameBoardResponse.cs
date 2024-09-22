using TodoList.UseCases.Boards;

namespace TodoList.Web.Boards.GetByName;

public class GetByNameBoardResponse
{
    public GetByNameBoardResponse()
    {
    }

    public GetByNameBoardResponse(Ulid id, string name, string title, string author, IEnumerable<ColumnDTO> columns)
    {
        Id = id;
        Name = name;
        Title = title;
        Author = author;
        Columns = columns;
    }

    public GetByNameBoardResponse(BoardDTO board)
    {
        Id = board.Id;
        Name = board.Name;
        Title = board.Title;
        Author = board.Author;
        Columns = board.Columns;
    }

    public Ulid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public IEnumerable<ColumnDTO> Columns { get; set; } = [];
}
