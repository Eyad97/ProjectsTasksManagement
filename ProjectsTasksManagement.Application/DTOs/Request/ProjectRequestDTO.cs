using System.ComponentModel.DataAnnotations;

namespace ProjectsTasksManagement.Application.DTOs.Request;

public class ProjectRequestDTO
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
}
