using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpForumSubscription
{
    public int SubscriptionId { get; set; }

    public int ForumId { get; set; }

    public int UserId { get; set; }

    public DateTime SubscribeDate { get; set; }

    public DateTime? UnSubscribeDate { get; set; }

    public Guid SubGuid { get; set; }
}
