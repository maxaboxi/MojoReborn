using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class I7SflexiItem
{
    public Guid SiteGuid { get; set; }

    public Guid FeatureGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public int ModuleId { get; set; }

    public Guid DefinitionGuid { get; set; }

    public Guid ItemGuid { get; set; }

    public int ItemId { get; set; }

    public int SortOrder { get; set; }

    public DateTime CreatedUtc { get; set; }

    public DateTime LastModUtc { get; set; }

    public string ViewRoles { get; set; } = null!;

    public string EditRoles { get; set; } = null!;
}
