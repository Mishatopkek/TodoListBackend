using Ardalis.Result;
using Ardalis.SharedKernel;

namespace TodoList.UseCases.Contributors.Get;

public record GetContributorQuery(int ContributorId) : IQuery<Result<ContributorDTO>>;
