namespace Mojo.Modules.Forum.Data;

public partial class ForumSubscription
{
    public int SubscriptionId { get; set; }

    public int ForumId { get; set; }

    public int UserId { get; set; }

    public DateTime SubscribeDate { get; set; }

    public DateTime? UnSubscribeDate { get; set; }

    public Guid SubGuid { get; set; }
}
