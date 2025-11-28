using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpUserClaim
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }
}
