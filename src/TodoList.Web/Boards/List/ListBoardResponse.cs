namespace TodoList.Web.Boards.List;

public class ListBoardResponse
{
    public ListBoardResponse()
    {
    }

    public ListBoardResponse(Ulid id, string name, string title, string author)
    {
        Id = id;
        Name = name;
        Title = title;
        Author = author;
    }

    public Ulid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
}
