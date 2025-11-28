using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpFriendlyUrl
{
    public int UrlId { get; set; }

    public int? SiteId { get; set; }

    public string? FriendlyUrl { get; set; }

    public string? RealUrl { get; set; }

    public bool IsPattern { get; set; }

    public Guid? PageGuid { get; set; }

    public Guid? SiteGuid { get; set; }

    public Guid? ItemGuid { get; set; }
}
