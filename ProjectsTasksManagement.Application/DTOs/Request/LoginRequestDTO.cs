using System.ComponentModel.DataAnnotations;

namespace ProjectsTasksManagement.Application.DTOs.Request;

public class LoginRequestDTO
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
