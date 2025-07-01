using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using ProjectsTasksManagement.Application.DTOs.Request;
using ProjectsTasksManagement.Application.DTOs.Response;
using ProjectsTasksManagement.Infrastructure.DBContext;
using ProjectsTasksManagement.Tests.Configs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProjectsTasksManagement.Tests;

public class TaskTest : IDisposable
{
    private readonly TestServer _server;
    private readonly TestDatabaseInitializer _testDatabaseInitializer;
    private readonly HttpClient _httpClient;
    private readonly LoginTest _loginTest;
    private readonly ProjectTest _projectTest;

    public TaskTest(HttpClient httpClient = null)
    {
        if (httpClient is null)
        {
            _server = TestServerFactory.CreateServer<ApplicationDbContext>();
            _testDatabaseInitializer = new TestDatabaseInitializer();
            httpClient = _server.CreateClient();
        }
        _httpClient = httpClient;
        _loginTest = new LoginTest(httpClient);
        _projectTest = new ProjectTest(httpClient);
    }

    private static readonly TaskRequestDTO taskRequestDTO = new()
    {
        Title = "Test API Task",
        DueDate = DateTime.Now.AddDays(1)
    };

    public async Task<TaskDTO> CreateAsync(Guid projectId)
    {
        using var response = await _httpClient.PostAsJsonAsync(Constants.BaseURL + "projects/" + projectId + "/tasks", taskRequestDTO);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseBody = await response.Content.ReadAsStringAsync();
        return Assert.IsType<TaskDTO>(JsonConvert.DeserializeObject<TaskDTO>(responseBody));
    }

    public async Task<List<TaskDTO>> GetByProjectAsync(Guid projectId)
    {
        using var response = await _httpClient.GetAsync(Constants.BaseURL + "projects/" + projectId.ToString() + "/tasks");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseBody = await response.Content.ReadAsStringAsync();
        return Assert.IsType<List<TaskDTO>>(JsonConvert.DeserializeObject<List<TaskDTO>>(responseBody));
    }

    [Fact]
    public async Task CreateTaskShouldReturnOKAsync()
    {
        try
        {
            await _loginTest.AdminLoginShouldReturnOKAsync();
            var project = await _projectTest.CreateAsync();
            await CreateAsync(project.Id);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Fact]
    public async Task GetTasksByProjectShouldReturnOKAsync()
    {
        try
        {
            await _loginTest.AdminLoginShouldReturnOKAsync();
            var project = await _projectTest.CreateAsync();
            await CreateAsync(project.Id);
            await CreateAsync(project.Id);
            await _loginTest.UserLoginShouldReturnOKAsync();
            await GetByProjectAsync(project.Id);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    public void Dispose()
    {
        _testDatabaseInitializer?.Dispose();
        _server?.Dispose();
        _httpClient?.Dispose();
    }
}
