using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsTasksManagement.Application.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    IQueryable<T> Table { get; }
    Task AddAsync(T entity);
    void Remove(T entity);
    Task<T> GetByIdAsync(Guid id);
}
