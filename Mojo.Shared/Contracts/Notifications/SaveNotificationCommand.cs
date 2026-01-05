namespace Mojo.Shared.Contracts.Notifications;

public record SaveNotificationCommand(Guid UserToNotify, Guid SourceModuleGuid, string Message, string TargetUrl, string FeatureName, Guid? EntityGuid, int? EntityId);