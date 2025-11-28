using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSiteSettingsExDef
{
    public string KeyName { get; set; } = null!;

    public string? GroupName { get; set; }

    public string? DefaultValue { get; set; }

    public int SortOrder { get; set; }
}
