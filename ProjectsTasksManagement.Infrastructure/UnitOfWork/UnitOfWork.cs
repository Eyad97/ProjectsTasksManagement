using ProjectsTasksManagement.Application.Interfaces.Repositories;
using ProjectsTasksManagement.Application.Interfaces.UnitOfWork;
using ProjectsTasksManagement.Infrastructure.DBContext;
using ProjectsTasksManagement.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace ProjectsTasksManagement.Infrastructure.UnitOfWork;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public IBaseRepository<T> Repository<T>() where T : class => new BaseRepository<T>(context);

    public IProjectRepository projectRepository => new ProjectRepository(context);

    public ITaskReposirory taskReposirory => new TaskRepository(context);

    public async Task SaveAsync() => await context.SaveChangesAsync();

    public async ValueTask DisposeAsync() => await context.DisposeAsync();

    public void Dispose() => context.Dispose();
}
