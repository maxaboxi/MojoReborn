using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpContentWorkflow
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public DateTime CreatedDateUtc { get; set; }

    public Guid UserGuid { get; set; }

    public Guid? LastModUserGuid { get; set; }

    public DateTime? LastModUtc { get; set; }

    public string Status { get; set; } = null!;

    public string? ContentText { get; set; }

    public string? CustomData { get; set; }

    public int? CustomReferenceNumber { get; set; }

    public Guid? CustomReferenceGuid { get; set; }

    public virtual ICollection<MpContentWorkflowAuditHistory> MpContentWorkflowAuditHistories { get; set; } = new List<MpContentWorkflowAuditHistory>();
}
