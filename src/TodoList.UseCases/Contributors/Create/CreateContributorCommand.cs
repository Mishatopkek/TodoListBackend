using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Contributors.Create;

/// <summary>
///     Create a new Contributor.
/// </summary>
/// <param name="Name"></param>
public record CreateContributorCommand(string Name) : ICommand<Result<int>>;
