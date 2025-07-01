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

[Route("api/projects")]
[ApiController]
public class ProjectController(IProjectService projectService) : ControllerBase
{
    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<ActionResult<ProjectDTO>> AddAsync(ProjectRequestDTO projectRequest)
    {
        var project = await projectService.AddAsync(projectRequest);
        return Ok(project);
    }

    [Authorize(Roles = Roles.User)]
    [HttpGet]
    public async Task<ActionResult<List<ProjectDTO>>> GetAllAsync()
    {
        var projects = await projectService.GetAllAsync();
        return Ok(projects);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{projectId}")]
    public async Task<IActionResult> RemoveAsync(Guid projectId)
    {
        await projectService.RemoveAsync(projectId);
        return NoContent();
    }
}
