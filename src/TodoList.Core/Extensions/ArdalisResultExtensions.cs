using Ardalis.Result;
using FastEndpoints;
using FluentValidation.Results;

namespace TodoList.Core.Extensions;

public static class ArdalisResultExtensions
{
    public static ErrorResponse ToErrorResponse<T>(this Result<T> result)
    {
        var errors = result
            .ValidationErrors
            .Select(x =>
                new ValidationFailure(x.Identifier, x.ErrorMessage)
            )
            .ToList();

        return new ErrorResponse(errors);
    }
}
