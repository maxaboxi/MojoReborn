using Mojo.Modules.Notifications.Data;
using Mojo.Modules.Notifications.Domain.Entities;
using Mojo.Modules.Notifications.Domain.Events;
using Mojo.Shared.Contracts.Notifications;

namespace Mojo.Modules.Notifications.Features.SaveNotification;

public class SaveNotificationHandler
{
    public static async Task<NotificationSaved> Handle(
        SaveNotificationCommand command,
        NotificationsDbContext db,
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
        }, ct);
        
        await db.SaveChangesAsync(ct);
        
        return new NotificationSaved(command.UserToNotify.ToString(), command.Message, command.TargetUrl, command.FeatureName);
    }
}