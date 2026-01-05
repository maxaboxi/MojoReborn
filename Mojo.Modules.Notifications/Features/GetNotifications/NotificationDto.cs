namespace Mojo.Modules.Notifications.Features.GetNotifications;

public record NotificationDto(Guid NotificationId, string Message, string Url, bool IsRead, DateTime CreatedAt);