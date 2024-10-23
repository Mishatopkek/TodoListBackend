using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TodoList.Infrastructure.Data;

public static class AppDbContextExtensions
{
    public static void AddApplicationDbContext(this IServiceCollection services, string connection)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connection));
    }
}
