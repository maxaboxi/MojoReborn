using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpPoll
{
    public Guid PollGuid { get; set; }

    public Guid SiteGuid { get; set; }

    public string Question { get; set; } = null!;

    public bool Active { get; set; }

    public bool AnonymousVoting { get; set; }

    public bool AllowViewingResultsBeforeVoting { get; set; }

    public bool ShowOrderNumbers { get; set; }

    public bool ShowResultsWhenDeactivated { get; set; }

    public DateTime ActiveFrom { get; set; }

    public DateTime ActiveTo { get; set; }
}
