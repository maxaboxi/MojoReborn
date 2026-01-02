using Microsoft.AspNetCore.SignalR;
using Mojo.Modules.Notifications.Data;
using Mojo.Modules.Notifications.Domain;
using Mojo.Modules.Notifications.Domain.Entities;
using Mojo.Shared.Contracts.Notifications;

namespace Mojo.Modules.Notifications.Features.SaveNotification;

public class SaveNotificationHandler
{
    public static async Task Handle(
        SaveNotificationCommand command,
        NotificationsDbContext db,
        IHubContext<NotificationsHub> hubContext,
        CancellationToken ct)
    {
        await db.UserNotifications.AddAsync(new UserNotification
        {
            UserId = command.UserToNotify,
            ModuleGuid = command.SourceModuleGuid,
            FeatureName = command.FeatureName,
            Message = command.Message,
            Url = command.TargetUrl,
            IsRead = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, ct);
        
        await db.SaveChangesAsync(ct);
        
        await hubContext.Clients.Group($"user:{command.UserToNotify}")
            .SendAsync("Notification", new {
                userId = command.UserToNotify.ToString(),
                message = command.Message,
                targetUrl = command.TargetUrl,
                featureName = command.FeatureName
            }, ct);
    }
}