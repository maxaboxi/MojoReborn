using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpContentWorkflowAuditHistory
{
    public Guid Guid { get; set; }

    public Guid ContentWorkflowGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public Guid UserGuid { get; set; }

    public DateTime CreatedDateUtc { get; set; }

    public string? NewStatus { get; set; }

    public string? Notes { get; set; }

    public bool? Active { get; set; }

    public virtual MpContentWorkflow ContentWorkflow { get; set; } = null!;
}
