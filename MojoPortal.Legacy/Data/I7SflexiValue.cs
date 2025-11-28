using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class I7SflexiValue
{
    public Guid ValueGuid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid FeatureGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public Guid ItemGuid { get; set; }

    public Guid FieldGuid { get; set; }

    public string FieldValue { get; set; } = null!;

    public int Id { get; set; }
}
