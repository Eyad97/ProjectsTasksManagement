using ProjectsTasksManagement.Application.DTOs.Request;
using ProjectsTasksManagement.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectsTasksManagement.Application.Interfaces.Services;

public interface ITaskService
{
    Task<TaskDTO> AddAsync(Guid projectId, TaskRequestDTO taskRequest);
    Task<List<TaskDTO>> GetByProjectAsync(Guid projectId);
    Task RemoveAsync(Guid taskId);
    Task ProcessOverdueTasksAsync();
}
