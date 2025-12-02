namespace Mojo.Modules.Forum.Data;

public partial class ForumThreadSubscription
{
    public int ThreadSubscriptionId { get; set; }

    public int ThreadId { get; set; }

    public int UserId { get; set; }

    public DateTime SubscribeDate { get; set; }

    public DateTime? UnSubscribeDate { get; set; }

    public Guid SubGuid { get; set; }

    public virtual ForumThread Thread { get; set; } = null!;
}
