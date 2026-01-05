namespace Mojo.Modules.Notifications.Domain.Entities;

public class UserNotification
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ModuleGuid { get; set; }
    public string FeatureName { get; set; } = string.Empty;
    public Guid? EntityGuid { get; set; }
    public int? EntityId { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}