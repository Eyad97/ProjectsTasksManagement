using System;
using System.Collections.Generic;

namespace ProjectsTasksManagement.Application.DTOs.Response;

public class ProjectDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<TaskDTO> Tasks { get; set; }
}
