using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wolverine;

namespace Mojo.Modules.Notifications.Features.DeleteNotifications;

public class DeleteNotificationsScheduler(IServiceScopeFactory scopeFactory, ILogger<DeleteNotificationsScheduler> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                logger.LogInformation("Running scheduled notification cleanup");
                
                using var scope = scopeFactory.CreateScope();
                var bus = scope.ServiceProvider.GetRequiredService<IMessageBus>();
                await bus.PublishAsync(new DeleteNotificationsEvent(30));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to publish DeleteNotificationsEvent");
            }
            
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}