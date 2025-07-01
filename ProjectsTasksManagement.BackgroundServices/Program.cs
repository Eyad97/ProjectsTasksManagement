using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectsTasksManagement.Application.Interfaces.Services;
using ProjectsTasksManagement.Application.Interfaces.UnitOfWork;
using ProjectsTasksManagement.Application.Mapper;
using ProjectsTasksManagement.Application.Services;
using ProjectsTasksManagement.Infrastructure.DBContext;
using ProjectsTasksManagement.Infrastructure.UnitOfWork;

namespace ProjectsTasksManagement.BackgroundServices;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;

        var configuration = builder.Configuration;

        services.Configure<HostOptions>(o => o.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore);

        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseMySql(configuration["ConnectionStrings:DBConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DBConnection"]));
        });

        services.AddAuthentication();

        services.AddMemoryCache();

        services.AddHttpContextAccessor();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IProjectService, ProjectService>();

        services.AddScoped<ITaskService, TaskService>();

        services.AddSingleton<IAuthService, AuthService>();

        services.AddSingleton(new MapperConfiguration(c => c.AddProfile(new MapperProfile())).CreateMapper());

        services.AddHostedService<OverdueTasksProcessingService>();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.MapGet("/", () => "Background services project is running...");

        app.Run();
    }
}
