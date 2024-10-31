namespace TodoList.Web.Boards.Column.Card.Details.GetByIdCardDetails;

public class GetByIdCardDetailsResponse
{
    public string? Description { get; set; }
    public IEnumerable<GetByIdCardDetailsResponseComment>? Comments { get; set; }
    public IEnumerable<GetByIdCardDetailsResponseUser>? Users { get; set; }
}

public class GetByIdCardDetailsResponseComment
{
    public Ulid Id { get; set; }
    public string Text { get; set; } = null!;
    public Ulid UserId { get; set; }
}

public class GetByIdCardDetailsResponseUser
{
    public Ulid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? ProfilePicture { get; set; }
    public DateTime CreationDate { get; set; }
}
