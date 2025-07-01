using Microsoft.AspNetCore.TestHost;
using ProjectsTasksManagement.Application.DTOs.Request;
using ProjectsTasksManagement.Infrastructure.DBContext;
using ProjectsTasksManagement.Tests.Configs;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProjectsTasksManagement.Tests;

public class LoginTest : IDisposable
{
    private readonly TestServer _server;
    private readonly TestDatabaseInitializer _testDatabaseInitializer;
    private readonly HttpClient _httpClient;

    public LoginTest(HttpClient httpClient = null)
    {
        if (httpClient is null)
        {
            _server = TestServerFactory.CreateServer<ApplicationDbContext>();
            _testDatabaseInitializer = new TestDatabaseInitializer();
            httpClient = _server.CreateClient();
        }
        _httpClient = httpClient;
    }

    private readonly LoginRequestDTO adminLoginRequest = new()
    {
        UserName = "Admin",
        Password = "Admin@123"
    };

    private readonly LoginRequestDTO userLoginRequest = new()
    {
        UserName = "User",
        Password = "User@123"
    };

    private async Task LoginAsync(bool isAdmin)
    {
        var response = await _httpClient.PostAsJsonAsync(Constants.BaseURL + "auth/login", isAdmin ? adminLoginRequest : userLoginRequest);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var token = await response.Content.ReadAsStringAsync();
        Assert.NotNull(token);
        _httpClient.DefaultRequestHeaders.Remove(nameof(HttpRequestHeader.Authorization));
        _httpClient.DefaultRequestHeaders.Add(nameof(HttpRequestHeader.Authorization), "Bearer " + token);
    }

    [Fact]
    public async Task AdminLoginShouldReturnOKAsync()
    {
        try
        {
            await LoginAsync(true);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.ToString());
        }
    }

    [Fact]
    public async Task UserLoginShouldReturnOKAsync()
    {
        try
        {
            await LoginAsync(false);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.ToString());
        }
    }

    public void Dispose()
    {
        _testDatabaseInitializer?.Dispose();
        _server?.Dispose();
        _httpClient?.Dispose();
    }
}
