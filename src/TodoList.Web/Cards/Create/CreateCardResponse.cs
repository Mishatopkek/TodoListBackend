namespace TodoList.Web.Cards.Create;

public class CreateCardResponse(int id, string name)
{
    public int Id { get; set; } = id;

    public string Name { get; set; } = name;
}
