using ProjectsTasksManagement.Application.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace ProjectsTasksManagement.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork: IAsyncDisposable, IDisposable
{
    IBaseRepository<T> Repository<T>() where T : class;
    IProjectRepository projectRepository {  get; }
    ITaskReposirory taskReposirory { get; }
    Task SaveAsync();
}
