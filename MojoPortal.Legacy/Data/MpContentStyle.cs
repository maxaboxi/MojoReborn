using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpContentStyle
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public string Name { get; set; } = null!;

    public string Element { get; set; } = null!;

    public string CssClass { get; set; } = null!;

    public string SkinName { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedUtc { get; set; }

    public DateTime LastModUtc { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid LastModBy { get; set; }
}
