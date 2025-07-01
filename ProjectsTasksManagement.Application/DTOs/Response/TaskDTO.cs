using System;

namespace ProjectsTasksManagement.Application.DTOs.Response;

public class TaskDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime DueDate { get; set; }
    public Guid ProjectId { get; set; }
}
