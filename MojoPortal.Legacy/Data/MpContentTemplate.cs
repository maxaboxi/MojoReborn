using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpContentTemplate
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public string Title { get; set; } = null!;

    public string? ImageFileName { get; set; }

    public string? Description { get; set; }

    public string? Body { get; set; }

    public string? AllowedRoles { get; set; }

    public Guid CreatedByUser { get; set; }

    public Guid LastModUser { get; set; }

    public DateTime CreatedUtc { get; set; }

    public DateTime LastModUtc { get; set; }
}
