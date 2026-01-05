using Microsoft.AspNetCore.SignalR;
using Mojo.Modules.Notifications.Data;
using Mojo.Modules.Notifications.Domain;
using Mojo.Modules.Notifications.Domain.Entities;
using Mojo.Modules.Notifications.Features.GetNotifications;
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
        var notification = new UserNotification
        {
            UserId = command.UserToNotify,
            ModuleGuid = command.SourceModuleGuid,
            FeatureName = command.FeatureName,
            Message = command.Message,
            Url = command.TargetUrl,
            IsRead = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            EntityGuid = command.EntityGuid,
            EntityId = command.EntityId
        };
        await db.UserNotifications.AddAsync(notification, ct);
        
        await db.SaveChangesAsync(ct);
        
        await hubContext.Clients.Group($"user:{command.UserToNotify}")
            .SendAsync("Notification", 
                new NotificationDto(
                    notification.Id, 
                    notification.Message, 
                    notification.Url,
                    notification.FeatureName,
                    false, 
                    notification.CreatedAt,
                    command.EntityGuid,
                    command.EntityId)
                ,ct);
    }
}