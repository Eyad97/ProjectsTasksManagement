using Microsoft.EntityFrameworkCore;
using ProjectsTasksManagement.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectsTasksManagement.Domain.Interfaces;

namespace ProjectsTasksManagement.Infrastructure.DBContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Server=localhost;Port=3306;User=root;Password=Root@963;Database=ProjectsTasksMgtDB;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
                    .Property(p => p.Name).IsRequired();

        modelBuilder.Entity<Project>()
                    .HasQueryFilter(f => !f.IsDeleted)
                    .HasIndex(r => r.IsDeleted)
                    .HasFilter("IsDeleted = 0");

        modelBuilder.Entity<Domain.Entities.Task>()
                    .Property(p => p.Title).IsRequired();

        modelBuilder.Entity<Domain.Entities.Task>()
                    .Property(p => p.Status).IsRequired();

        modelBuilder.Entity<Domain.Entities.Task>()
                    .Property(p => p.DueDate).HasColumnType("datetime");

        modelBuilder.Entity<Domain.Entities.Task>()
                    .HasQueryFilter(f => !f.IsDeleted)
                    .HasIndex(r => r.IsDeleted)
                    .HasFilter("IsDeleted = 0");
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Domain.Entities.Task> Tasks { get; set; }

    private void HandleSoftDelete()
    {
        var entityEntries = ChangeTracker.Entries<ISoftDeletable>()
                                         .Where(f => f.State == EntityState.Deleted);

        if (entityEntries != null)
        {
            foreach (var entity in entityEntries)
            {
                entity.Entity.IsDeleted = true;
                entity.State = EntityState.Modified;
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        HandleSoftDelete();
        return await base.SaveChangesAsync(cancellationToken);
    }
}
