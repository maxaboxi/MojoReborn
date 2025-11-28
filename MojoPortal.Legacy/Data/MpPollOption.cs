using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpPollOption
{
    public Guid OptionGuid { get; set; }

    public Guid PollGuid { get; set; }

    public string Answer { get; set; } = null!;

    public int Votes { get; set; }

    public int Order { get; set; }
}
