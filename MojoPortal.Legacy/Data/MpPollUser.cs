using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpPollUser
{
    public Guid PollGuid { get; set; }

    public Guid UserGuid { get; set; }

    public Guid OptionGuid { get; set; }
}
