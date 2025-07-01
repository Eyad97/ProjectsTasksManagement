using Microsoft.AspNetCore.Mvc;
using ProjectsTasksManagement.Application.DTOs.Request;
using ProjectsTasksManagement.Application.Interfaces.Services;

namespace ProjectsTasksManagement.WebApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(LoginRequestDTO loginDTO)
    {
        var token = authService.Login(loginDTO);

        if (string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized();
        }

        return Ok(token);
    }
}
