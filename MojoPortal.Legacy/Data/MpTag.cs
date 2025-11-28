using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpTag
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid FeatureGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public string Tag { get; set; } = null!;

    public DateTime CreatedUtc { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedUtc { get; set; }

    public Guid ModifiedBy { get; set; }

    public int ItemCount { get; set; }

    public Guid? VocabularyGuid { get; set; }

    public virtual ICollection<MpTagItem> MpTagItems { get; set; } = new List<MpTagItem>();
}
