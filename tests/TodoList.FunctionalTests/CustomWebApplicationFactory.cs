using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestSharp;
using TodoList.Infrastructure.Data;

namespace TodoList.FunctionalTests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    /// <summary>
    ///     Overriding CreateHost to avoid creating a separate ServiceProvider per this thread:
    ///     https://github.com/dotnet-architecture/eShopOnWeb/issues/465
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    protected override IHost CreateHost(IHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("JWT_SECRET", "1234567890JWT_SECRET_VALUE1234567890");
        Environment.SetEnvironmentVariable("PASSWORD_SALT_SECRET", "1234567890PASSWORD_SALT_SECRET_VALUE1234567890");

        builder.UseEnvironment("Development"); // will not send real emails
        IHost host = builder.Build();
        host.Start();

        // Get service provider.
        IServiceProvider serviceProvider = host.Services;

        // Create a scope to obtain a reference to the database
        // context (AppDbContext).
        using IServiceScope scope = serviceProvider.CreateScope();
        IServiceProvider scopedServices = scope.ServiceProvider;
        AppDbContext db = scopedServices.GetRequiredService<AppDbContext>();

        ILogger<CustomWebApplicationFactory<TProgram>> logger = scopedServices
            .GetRequiredService<ILogger<CustomWebApplicationFactory<TProgram>>>();

        // Reset Sqlite database for each test run
        // If using a real database, you'll likely want to remove this step.
        db.Database.EnsureDeleted();

        // Ensure the database is created.
        db.Database.EnsureCreated();

        // Can also skip creating the items
        //if (!db.ToDoItems.Any())
        //{
        // Seed the database with test data.
        SeedData.PopulateTestData(db);
        //}

        return host;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(services =>
            {
                // Configure test dependencies here

                //// Remove the app's ApplicationDbContext registration.
                ServiceDescriptor? descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                //// This should be set for each individual test run
                var inMemoryCollectionName = Guid.NewGuid().ToString();

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase(inMemoryCollectionName);
                });
            });
    }

    public RestClient CreateRestClient()
    {
        // Create the HttpClient using the base factory
        HttpClient client = CreateClient();

        // Configure the RestClient with the desired base URL
        var restClient =
            new RestClient(client, new RestClientOptions {BaseUrl = new Uri(client.BaseAddress!, "/api/")});

        return restClient;
    }
}
