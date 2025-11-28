using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpCategoryItem
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid FeatureGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public Guid ItemGuid { get; set; }

    public Guid CategoryGuid { get; set; }

    public Guid ExtraGuid { get; set; }
}
