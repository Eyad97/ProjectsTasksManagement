using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using ProjectsTasksManagement.WebApi;

namespace ProjectsTasksManagement.Tests.Configs;

public static class TestServerFactory
{
    public static TestServer CreateServer<TContext>() where TContext : DbContext
    {
        IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");

        var builder = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder

            .ConfigureServices(services =>
            {
                // Replace the existing DbContext registration with the desired context
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<TContext>(options =>
                {
                    options.UseMySql(config["ConnectionStrings:DBConnectionTesting"], ServerVersion.AutoDetect(config["ConnectionStrings:DBConnectionTesting"]));
                });

                // Inject the TestDatabaseInitializer
                services.AddSingleton<TestDatabaseInitializer>();
                services.AddSingleton(config);
            });
            });

        return builder.Server;
    }
}
