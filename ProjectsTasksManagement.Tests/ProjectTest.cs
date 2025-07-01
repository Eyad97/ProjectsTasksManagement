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

public class ProjectTest : IDisposable
{
    private readonly TestServer _server;
    private readonly TestDatabaseInitializer _testDatabaseInitializer;
    private readonly HttpClient _httpClient;
    private readonly LoginTest _loginTest;

    public ProjectTest(HttpClient httpClient = null)
    {
        if (httpClient is null)
        {
            _server = TestServerFactory.CreateServer<ApplicationDbContext>();
            _testDatabaseInitializer = new TestDatabaseInitializer();
            httpClient = _server.CreateClient();
        }
        _httpClient = httpClient;
        _loginTest = new LoginTest(httpClient);
    }

    private static readonly ProjectRequestDTO projectRequestDTO = new()
    {
        Name = "Test API Project",
        Description = "Test API Project"
    };

    public async Task<ProjectDTO> CreateAsync()
    {
        using var response = await _httpClient.PostAsJsonAsync(Constants.BaseURL + "projects", projectRequestDTO);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseBody = await response.Content.ReadAsStringAsync();
        return Assert.IsType<ProjectDTO>(JsonConvert.DeserializeObject<ProjectDTO>(responseBody));
    }

    public async Task<List<ProjectDTO>> GetAsync()
    {
        using var response = await _httpClient.GetAsync(Constants.BaseURL + "projects");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseBody = await response.Content.ReadAsStringAsync();
        return Assert.IsType<List<ProjectDTO>>(JsonConvert.DeserializeObject<List<ProjectDTO>>(responseBody));
    }

    [Fact]
    public async Task CreateProjectShouldReturnOKAsync()
    {
        try
        {
            await _loginTest.AdminLoginShouldReturnOKAsync();
            await CreateAsync();
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Fact]
    public async Task GetProjectsShouldReturnOKAsync()
    {
        try
        {
            await _loginTest.AdminLoginShouldReturnOKAsync();
            await CreateAsync();
            await CreateAsync();
            await CreateAsync();
            await _loginTest.UserLoginShouldReturnOKAsync();
            await GetAsync();
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
