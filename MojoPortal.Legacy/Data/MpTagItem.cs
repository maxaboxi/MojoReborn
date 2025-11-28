using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpTagItem
{
    public Guid TagItemGuid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid FeatureGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public Guid RelatedItemGuid { get; set; }

    public Guid TagGuid { get; set; }

    public Guid ExtraGuid { get; set; }

    public Guid TaggedBy { get; set; }

    public virtual MpTag Tag { get; set; } = null!;
}
