using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectsTasksManagement.Infrastructure.DBContext;
using System;

namespace ProjectsTasksManagement.Tests.Configs;

public class TestDatabaseInitializer : IDisposable
{
    private readonly DbContextOptions<ApplicationDbContext> _options;
    private readonly ApplicationDbContext _context;

    public TestDatabaseInitializer()
    {
        // Configure the DbContext options
        IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseMySql(config["ConnectionStrings:DBConnectionTesting"], ServerVersion.AutoDetect(config["ConnectionStrings:DBConnectionTesting"]))
            .Options;

        // Create a new instance of your DbContext
        _context = new ApplicationDbContext(_options);
        System.Diagnostics.Debug.WriteLine("new instance of ApplicationDbContext is created..." + DateTime.Now);
        _context.Database.EnsureDeleted();
        System.Diagnostics.Debug.WriteLine("EnsureDeleted..." + DateTime.Now);
        // Apply the migrations
        _context.Database.Migrate();
        System.Diagnostics.Debug.WriteLine("Migrate..." + DateTime.Now);
        _context.Database.EnsureCreated();
        System.Diagnostics.Debug.WriteLine("EnsureCreated..." + DateTime.Now);
    }

    public void Dispose()
    {
        // Clean up the database after the test is done
        _context.Database.EnsureDeleted();
        System.Diagnostics.Debug.WriteLine("EnsureDeleted..." + DateTime.Now);
        _context.Dispose();
        System.Diagnostics.Debug.WriteLine("Dispose..." + DateTime.Now);
    }
}
