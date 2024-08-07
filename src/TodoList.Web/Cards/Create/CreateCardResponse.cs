namespace TodoList.Web.Cards.Create;

public class CreateCardResponse(Ulid id, string name)
{
    public Ulid Id { get; set; } = id;

    public string Name { get; set; } = name;
}
