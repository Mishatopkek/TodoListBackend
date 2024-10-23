using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TodoList.Infrastructure.Data;

public static class AppDbContextExtensions
{
    public static void AddApplicationDbContext(this IServiceCollection services, string password)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql($"Host=localhost;Database=todolist;Username=postgres;Password={password};"));
    }
}
