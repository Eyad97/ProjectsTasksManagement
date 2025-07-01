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

public class TaskService(IUnitOfWork unitOfWork, IMapper mapper) : ITaskService
{
    public async Task<TaskDTO> AddAsync(Guid projectId, TaskRequestDTO taskModel)
    {
        var task = new Domain.Entities.Task(taskModel.Title, taskModel.DueDate, projectId);
        await unitOfWork.taskReposirory.AddAsync(task);
        await unitOfWork.SaveAsync();
        return mapper.Map<TaskDTO>(task);
    }

    public async Task<List<TaskDTO>> GetByProjectAsync(Guid projectId)
    {
        var tasks = await unitOfWork.taskReposirory.GetByProjectAsync(projectId);
        return [.. tasks.Select(mapper.Map<TaskDTO>)];
    }

    public async Task RemoveAsync(Guid taskId)
    {
        var task = await unitOfWork.taskReposirory.GetByIdAsync(taskId);
        unitOfWork.taskReposirory.Remove(task);
        await unitOfWork.SaveAsync();
    }

    public async Task ProcessOverdueTasksAsync()
    {
        var overdueTasks = await unitOfWork.taskReposirory.GetAllOverdueAsync();

        if (overdueTasks.Count == 0)
        {
            return;
        }

        foreach (var overdueTask in overdueTasks)
        {
            overdueTask.SetStatus(Domain.Constants.TaskStatus.Overdue);
        }

        await unitOfWork.SaveAsync();
    }
}
