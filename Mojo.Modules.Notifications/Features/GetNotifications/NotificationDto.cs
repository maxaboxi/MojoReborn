namespace Mojo.Modules.Notifications.Features.GetNotifications;

public record NotificationDto(Guid NotificationId, string Message, string Url, string FeatureName, bool IsRead, DateTime CreatedAt, Guid? EntityGuid, int? EntityId);