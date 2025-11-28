using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSiteFolder
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public string FolderName { get; set; } = null!;
}
