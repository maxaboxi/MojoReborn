namespace Mojo.Modules.Forum.Domain.Entities;

public class ForumThreadSubscription
{
    public int Id { get; set; }

    public int ThreadId { get; set; }

    public int UserId { get; set; }

    public DateTime SubscribeDate { get; set; }

    public DateTime? UnSubscribeDate { get; set; }

    public Guid SubscriptionGuid { get; set; }

    public virtual ForumThread Thread { get; set; } = null!;
}