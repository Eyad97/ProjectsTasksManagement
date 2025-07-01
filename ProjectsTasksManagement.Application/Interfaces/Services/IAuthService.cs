using ProjectsTasksManagement.Application.DTOs.Request;

namespace ProjectsTasksManagement.Application.Interfaces.Services;

public interface IAuthService
{
    string Login(LoginRequestDTO loginDTO);
}
