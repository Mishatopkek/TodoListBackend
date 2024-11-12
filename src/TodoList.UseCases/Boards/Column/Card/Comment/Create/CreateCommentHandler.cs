using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Card.Comment.Create;

public class CreateCommentHandler(ICreateCommentService service) : ICommandHandler<CreateCommentCommand, Result<Ulid>>
{
    public async Task<Result<Ulid>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Ulid id = await service.CreateAsync(request.CardId, request.UserId, request.Text);
            return Result.Success(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result.Error(e.Message);
        }
    }
}
