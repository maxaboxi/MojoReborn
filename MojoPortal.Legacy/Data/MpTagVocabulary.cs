using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpTagVocabulary
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid FeatureGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedUtc { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedUtc { get; set; }

    public Guid ModifiedBy { get; set; }
}
