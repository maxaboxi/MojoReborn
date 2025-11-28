using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpContentMetum
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public Guid ContentGuid { get; set; }

    public string Name { get; set; } = null!;

    public string Scheme { get; set; } = null!;

    public string? LangCode { get; set; }

    public string? Dir { get; set; }

    public string? MetaContent { get; set; }

    public int SortRank { get; set; }

    public DateTime CreatedUtc { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime LastModUtc { get; set; }

    public Guid LastModBy { get; set; }

    public string NameProperty { get; set; } = null!;

    public string ContentProperty { get; set; } = null!;
}
