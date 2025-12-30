namespace Mojo.Shared.Contracts.Notifications;

public record SendNotificationCommand(Guid UserToNotify, string Message, string TargetUrl, string SourceModule);