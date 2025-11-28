using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpModuleDefinition
{
    public int ModuleDefId { get; set; }

    public string FeatureName { get; set; } = null!;

    public string ControlSrc { get; set; } = null!;

    public int SortOrder { get; set; }

    public bool IsAdmin { get; set; }

    public string? Icon { get; set; }

    public int DefaultCacheTime { get; set; }

    public Guid Guid { get; set; }

    public string? ResourceFile { get; set; }

    public bool? IsCacheable { get; set; }

    public bool? IsSearchable { get; set; }

    public string? SearchListName { get; set; }

    public bool? SupportsPageReuse { get; set; }

    public string? DeleteProvider { get; set; }

    public string? PartialView { get; set; }

    public string? SkinFileName { get; set; }

    public virtual ICollection<MpModule> MpModules { get; set; } = new List<MpModule>();
}
