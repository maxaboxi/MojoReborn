using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpContentHistory
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid UserGuid { get; set; }

    public Guid ContentGuid { get; set; }

    public string? Title { get; set; }

    public string? ContentText { get; set; }

    public string? CustomData { get; set; }

    public DateTime CreatedUtc { get; set; }

    public DateTime HistoryUtc { get; set; }
}
