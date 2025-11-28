using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpContentMetaLink
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public Guid ContentGuid { get; set; }

    public string Rel { get; set; } = null!;

    public string Href { get; set; } = null!;

    public string? HrefLang { get; set; }

    public string? Rev { get; set; }

    public string? Type { get; set; }

    public string? Media { get; set; }

    public int SortRank { get; set; }

    public DateTime CreatedUtc { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime LastModUtc { get; set; }

    public Guid LastModBy { get; set; }
}
