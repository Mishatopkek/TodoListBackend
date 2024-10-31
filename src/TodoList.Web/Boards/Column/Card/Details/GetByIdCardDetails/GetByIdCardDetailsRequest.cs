using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Boards.Column.Card.Details.GetByIdCardDetails;

public class GetByIdCardDetailsRequest
{
    public const string Route = "/Board/Column/Card/Details/GetById/{Id}";

    [Required] public Ulid Id { get; set; }
}
