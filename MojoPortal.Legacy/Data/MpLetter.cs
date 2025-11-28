using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpLetter
{
    public Guid LetterGuid { get; set; }

    public Guid LetterInfoGuid { get; set; }

    public string Subject { get; set; } = null!;

    public string? HtmlBody { get; set; }

    public string? TextBody { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedUtc { get; set; }

    public Guid LastModBy { get; set; }

    public DateTime LastModUtc { get; set; }

    public bool IsApproved { get; set; }

    public Guid ApprovedBy { get; set; }

    public DateTime? SendClickedUtc { get; set; }

    public DateTime? SendStartedUtc { get; set; }

    public DateTime? SendCompleteUtc { get; set; }

    public int SendCount { get; set; }

    public virtual MpLetterInfo LetterInfo { get; set; } = null!;
}
