using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSiteHost
{
    public int HostId { get; set; }

    public int SiteId { get; set; }

    public string HostName { get; set; } = null!;

    public Guid? SiteGuid { get; set; }
}
