namespace TodoList.Web.Cards.GetById;

public class GetByIdCardRequest
{
    public const string Route = "/Cards/{CardId:int}";

    public int CardId { get; set; }

    public static string BuildRoute(int cardId)
    {
        return Route.Replace("{CardId:int}", cardId.ToString());
    }
}
