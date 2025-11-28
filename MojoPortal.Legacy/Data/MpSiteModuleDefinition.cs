using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSiteModuleDefinition
{
    public int SiteId { get; set; }

    public int ModuleDefId { get; set; }

    public string? AuthorizedRoles { get; set; }

    public Guid? SiteGuid { get; set; }

    public Guid? FeatureGuid { get; set; }
}
