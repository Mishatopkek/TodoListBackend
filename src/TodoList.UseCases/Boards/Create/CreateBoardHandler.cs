﻿using Ardalis.Result;
using Ardalis.SharedKernel;
using TodoList.Core.BoardAggregate;
using TodoList.Core.BoardAggregate.Specifications;

namespace TodoList.UseCases.Boards.Create;

public class CreateBoardHandler(IRepository<Board> repository) : ICommandHandler<CreateBoardCommand, Result<bool>>
{
    private const string BoardNameExist = "Board is already exist. Try another name";

    public async Task<Result<bool>> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        var checkIfBoardExist =
            await repository.AnyAsync(new CreateBoardSpec(request.Name, request.UserId), cancellationToken);

        if (checkIfBoardExist)
        {
            List<ValidationError> errors =
            [
                new ValidationError(nameof(request.Name), BoardNameExist, "", ValidationSeverity.Error)
            ];
            return Result.Invalid(errors);
        }

        Board board = new()
        {
            Name = request.Name,
            Title = request.Title,
            UserId = request.UserId.ToGuid(),
            Columns =
            [
                new Core.BoardAggregate.Column {Title = "Todo", IsAlwaysVisibleAddCardButton = true, Order = 0},
                new Core.BoardAggregate.Column {Title = "Doing", Order = 1},
                new Core.BoardAggregate.Column {Title = "Done", Order = 2}
            ]
        };

        await repository.AddAsync(board, cancellationToken);
        return Result<bool>.Created(true);
    }
}
