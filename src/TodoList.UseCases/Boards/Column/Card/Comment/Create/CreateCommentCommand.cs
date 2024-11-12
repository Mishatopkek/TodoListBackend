using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Boards.Column.Card.Comment.Create;

public record CreateCommentCommand(Ulid CardId, Ulid UserId, string Text) : ICommand<Result<Ulid>>;
