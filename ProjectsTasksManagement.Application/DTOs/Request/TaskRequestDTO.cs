using System.ComponentModel.DataAnnotations;
using System;

namespace ProjectsTasksManagement.Application.DTOs.Request;

public class TaskRequestDTO
{
    [Required]
    public string Title { get; set; }
    public DateTime DueDate { get; set; }
}
