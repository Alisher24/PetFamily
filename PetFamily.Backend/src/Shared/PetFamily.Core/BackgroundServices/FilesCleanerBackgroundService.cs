using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using PetFamily.Core.Files;

namespace PetFamily.Core.BackgroundServices;

public class FilesCleanerBackgroundService(
    ILogger<FilesCleanerBackgroundService> logger,
    IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("FilesCleanerBackgroundService is starting");

        await using var scope = scopeFactory.CreateAsyncScope();

        var filesCleanerService = scope.ServiceProvider.GetRequiredService<IFilesCleanerService>();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await filesCleanerService.Process(stoppingToken);
        }

        await Task.CompletedTask;
    }
}