using ProjectsTasksManagement.Application.DTOs.Request;
using ProjectsTasksManagement.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectsTasksManagement.Application.Interfaces.Services;

public interface IProjectService
{
    Task<ProjectDTO> AddAsync(ProjectRequestDTO projectRequest);
    Task<List<ProjectDTO>> GetAllAsync();
    Task RemoveAsync(Guid projectId);
}
