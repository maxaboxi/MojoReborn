using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpUserLogin
{
    public string LoginProvider { get; set; } = null!;

    public string ProviderKey { get; set; } = null!;

    public string UserId { get; set; } = null!;
}
