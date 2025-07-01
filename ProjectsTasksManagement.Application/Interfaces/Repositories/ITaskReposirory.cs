using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectsTasksManagement.Application.Interfaces.Repositories;

public interface ITaskReposirory : IBaseRepository<Domain.Entities.Task>
{
    Task<List<Domain.Entities.Task>> GetByProjectAsync(Guid projectId);
    Task<List<Domain.Entities.Task>> GetAllOverdueAsync();
}
