using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpBannedIpaddress
{
    public int RowId { get; set; }

    public string BannedIp { get; set; } = null!;

    public DateTime BannedUtc { get; set; }

    public string? BannedReason { get; set; }
}
