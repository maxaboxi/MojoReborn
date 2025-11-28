using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpIndexingQueue
{
    public long RowId { get; set; }

    public string IndexPath { get; set; } = null!;

    public string? SerializedItem { get; set; }

    public string ItemKey { get; set; } = null!;

    public bool RemoveOnly { get; set; }

    public int SiteId { get; set; }
}
