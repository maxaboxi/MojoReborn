using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpForumThreadSubscription
{
    public int ThreadSubscriptionId { get; set; }

    public int ThreadId { get; set; }

    public int UserId { get; set; }

    public DateTime SubscribeDate { get; set; }

    public DateTime? UnSubscribeDate { get; set; }

    public Guid SubGuid { get; set; }

    public virtual MpForumThread Thread { get; set; } = null!;
}
