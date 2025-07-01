using ProjectsTasksManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ProjectsTasksManagement.Application.Interfaces.Repositories;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<List<Project>> GetAllAsync();
}
