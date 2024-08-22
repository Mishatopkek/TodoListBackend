using Microsoft.EntityFrameworkCore;
using TodoList.UseCases.Contributors;
using TodoList.UseCases.Contributors.List;

namespace TodoList.Infrastructure.Data.Queries;

public class ListContributorsQueryService(AppDbContext _db) : IListContributorsQueryService
{
    // You can use EF, Dapper, SqlClient, etc. for queries - this is just an example

    public async Task<IEnumerable<ContributorDTO>> ListAsync()
    {
        if (_db.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
        {
            // Use LINQ for the InMemory provider
            List<ContributorDTO> result = await _db.Contributors
                .Select(c => new ContributorDTO(c.Id, c.Name))
                .ToListAsync();

            return result;
        }
        else
        {
            // NOTE: This will fail if testing with EF InMemory provider
            // Use raw SQL for other providers
            List<ContributorDTO> result = await _db.Contributors
                .FromSqlRaw("SELECT Id, Name FROM Contributors")
                .Select(c => new ContributorDTO(c.Id, c.Name))
                .ToListAsync();

            return result;
        }
    }
}
