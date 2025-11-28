using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSiteSettingsEx
{
    public int SiteId { get; set; }

    public Guid SiteGuid { get; set; }

    public string KeyName { get; set; } = null!;

    public string? KeyValue { get; set; }

    public string? GroupName { get; set; }
}
