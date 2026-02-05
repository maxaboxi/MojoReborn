namespace Mojo.Modules.Forum.Domain.Entities;

public class ForumSubscription
{
    public int Id { get; set; }

    public int ForumId { get; set; }

    public int UserId { get; set; }

    public DateTime SubscribeDate { get; set; }

    public DateTime? UnSubscribeDate { get; set; }

    public Guid SubscriptionGuid { get; set; }
}