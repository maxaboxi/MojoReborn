using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpComment
{
    public Guid Guid { get; set; }

    public Guid ParentGuid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid FeatureGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public Guid ContentGuid { get; set; }

    public Guid UserGuid { get; set; }

    public string? Title { get; set; }

    public string UserComment { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string? UserUrl { get; set; }

    public string UserIp { get; set; } = null!;

    public DateTime CreatedUtc { get; set; }

    public DateTime LastModUtc { get; set; }

    public byte ModerationStatus { get; set; }

    public Guid ModeratedBy { get; set; }

    public string? ModerationReason { get; set; }
}
