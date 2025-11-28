using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpPageModule
{
    public int PageId { get; set; }

    public int ModuleId { get; set; }

    public string PaneName { get; set; } = null!;

    public int ModuleOrder { get; set; }

    public DateTime PublishBeginDate { get; set; }

    public DateTime? PublishEndDate { get; set; }

    public Guid? PageGuid { get; set; }

    public Guid? ModuleGuid { get; set; }

    public virtual MpModule Module { get; set; } = null!;

    public virtual MpPage Page { get; set; } = null!;
}
