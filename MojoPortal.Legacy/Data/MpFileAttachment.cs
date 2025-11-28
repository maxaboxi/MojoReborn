using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpFileAttachment
{
    public Guid RowGuid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public Guid ItemGuid { get; set; }

    public Guid SpecialGuid1 { get; set; }

    public Guid SpecialGuid2 { get; set; }

    public string ServerFileName { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public DateTime CreatedUtc { get; set; }

    public Guid CreatedBy { get; set; }

    public long? ContentLength { get; set; }

    public string? ContentType { get; set; }

    public string? ContentTitle { get; set; }
}
