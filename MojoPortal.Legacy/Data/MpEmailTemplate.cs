using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpEmailTemplate
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid FeatureGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public Guid SpecialGuid1 { get; set; }

    public Guid SpecialGuid2 { get; set; }

    public string Name { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string? TextBody { get; set; }

    public string? HtmlBody { get; set; }

    public bool HasHtml { get; set; }

    public bool IsEditable { get; set; }

    public DateTime CreatedUtc { get; set; }

    public DateTime LastModUtc { get; set; }

    public Guid LastModBy { get; set; }
}
