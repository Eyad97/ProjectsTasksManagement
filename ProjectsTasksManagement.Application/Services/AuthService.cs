using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectsTasksManagement.Application.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using ProjectsTasksManagement.Application.DTOs.Request;
using ProjectsTasksManagement.Domain.Constants;

namespace ProjectsTasksManagement.Application.Services;

public class AuthService(IConfiguration configuration) : IAuthService
{
    public string Login(LoginRequestDTO loginDTO)
    {
        if (loginDTO.UserName.ToLower().Equals("admin") && loginDTO.Password.Equals("Admin@123"))
        {
            return GenerateToken(loginDTO.UserName, Roles.Admin);
        }

        if (loginDTO.UserName.ToLower().Equals("user") && loginDTO.Password.Equals("User@123"))
        {
            return GenerateToken(loginDTO.UserName, Roles.User);
        }

        return null;
    }

    private string GenerateToken(string username, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecurityKey"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JWT:ValidIssuer"],
            audience: configuration["JWT:ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
