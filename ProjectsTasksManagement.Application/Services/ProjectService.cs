using AutoMapper;
using ProjectsTasksManagement.Application.DTOs.Request;
using ProjectsTasksManagement.Application.DTOs.Response;
using ProjectsTasksManagement.Application.Interfaces.Services;
using ProjectsTasksManagement.Application.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsTasksManagement.Application.Services;

public class ProjectService(IUnitOfWork unitOfWork, IMapper mapper) : IProjectService
{
    public async Task<ProjectDTO> AddAsync(ProjectRequestDTO projectModel)
    {
        var project = new Domain.Entities.Project(projectModel.Name, projectModel.Description);
        await unitOfWork.projectRepository.AddAsync(project);
        await unitOfWork.SaveAsync();
        return mapper.Map<ProjectDTO>(project);
    }

    public async Task<List<ProjectDTO>> GetAllAsync()
    {
        var projects = await unitOfWork.projectRepository.GetAllAsync();
        return [.. projects.Select(mapper.Map<ProjectDTO>)];
    }

    public async Task RemoveAsync(Guid ProjectId)
    {
        var project = await unitOfWork.projectRepository.GetByIdAsync(ProjectId);
        var tasks = await unitOfWork.taskReposirory.GetByProjectAsync(ProjectId);
        foreach (var task in tasks)
        {
            unitOfWork.taskReposirory.Remove(task);
        }
        unitOfWork.projectRepository.Remove(project);
        await unitOfWork.SaveAsync();
    }
}
