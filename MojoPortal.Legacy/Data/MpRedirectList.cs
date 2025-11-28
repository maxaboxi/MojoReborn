using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpRedirectList
{
    public Guid RowGuid { get; set; }

    public Guid SiteGuid { get; set; }

    public int SiteId { get; set; }

    public string OldUrl { get; set; } = null!;

    public string NewUrl { get; set; } = null!;

    public DateTime CreatedUtc { get; set; }

    public DateTime ExpireUtc { get; set; }
}
