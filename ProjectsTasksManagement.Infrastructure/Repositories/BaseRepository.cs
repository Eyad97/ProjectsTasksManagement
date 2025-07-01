using ProjectsTasksManagement.Application.Interfaces.Repositories;
using ProjectsTasksManagement.Infrastructure.DBContext;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsTasksManagement.Infrastructure.Repositories;

public class BaseRepository<T>(ApplicationDbContext context) : IBaseRepository<T> where T : class
{
    public IQueryable<T> Table => context.Set<T>();

    public async Task AddAsync(T entity)
    {
        await context.AddAsync(entity);
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public void Remove(T entity)
    {
        context.Set<T>().Remove(entity);
    }
}
