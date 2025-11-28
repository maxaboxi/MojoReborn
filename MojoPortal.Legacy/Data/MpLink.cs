using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpLink
{
    public int ItemId { get; set; }

    public int ModuleId { get; set; }

    public string? Title { get; set; }

    public string Url { get; set; } = null!;

    public string Target { get; set; } = null!;

    public int? ViewOrder { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public Guid? ItemGuid { get; set; }

    public Guid? ModuleGuid { get; set; }

    public Guid? UserGuid { get; set; }

    public virtual MpModule Module { get; set; } = null!;
}
