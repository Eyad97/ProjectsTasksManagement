using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectsTasksManagement.Application.DTOs.Request;
using ProjectsTasksManagement.Application.DTOs.Response;
using ProjectsTasksManagement.Application.Interfaces.Services;
using ProjectsTasksManagement.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectsTasksManagement.WebApi.Controllers;

[Route("api/projects/{projectId}/tasks")]
[ApiController]
public class TaskController(ITaskService taskService) : ControllerBase
{
    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<ActionResult<TaskDTO>> AddAsync(Guid projectId, TaskRequestDTO taskRequest)
    {
        var task = await taskService.AddAsync(projectId, taskRequest);
        return Ok(task);
    }

    [Authorize(Roles = Roles.User)]
    [HttpGet]
    public async Task<ActionResult<List<TaskDTO>>> GetByProjectAsync(Guid projectId)
    {
        var tasks = await taskService.GetByProjectAsync(projectId);
        return Ok(tasks);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{taskId}")]
    public async Task<IActionResult> RemoveAsync(Guid taskId)
    {
        await taskService.RemoveAsync(taskId);
        return NoContent();
    }
}
