using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Contributors.Delete;

public record DeleteContributorCommand(int ContributorId) : ICommand<Result>;
