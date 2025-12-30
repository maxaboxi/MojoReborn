using Mojo.Shared.Interfaces.WebSocket;

namespace Mojo.Modules.Notifications.Domain.Events;

public record NotificationSaved(string UserId, string Message, string TargetUrl, string FeatureName) : ISignalRMessage;