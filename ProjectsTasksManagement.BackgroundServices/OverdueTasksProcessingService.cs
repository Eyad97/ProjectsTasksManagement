using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectsTasksManagement.Application.Interfaces.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectsTasksManagement.BackgroundServices;

public class OverdueTasksProcessingService(ILogger<OverdueTasksProcessingService> logger, IServiceProvider serviceProvider) : BackgroundService
{
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting overdue tasks processing service...");
        await base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping overdue tasks processing service...");
        await base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        do
        {
            try
            {
                logger.LogInformation("Overdue tasks processing service: executed at ({datetime})", DateTime.Now);
                using var scope = serviceProvider.CreateScope();
                var taskService = scope.ServiceProvider.GetService<ITaskService>();
                await taskService.ProcessOverdueTasksAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("Overdue tasks processing service: ex {ex}", ex.Message);
            }

            await Task.Delay(600000, cancellationToken);
        }
        while (!cancellationToken.IsCancellationRequested);
    }
}
