using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpLetterSubscribe
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid LetterInfoGuid { get; set; }

    public Guid UserGuid { get; set; }

    public string Email { get; set; } = null!;

    public bool IsVerified { get; set; }

    public Guid VerifyGuid { get; set; }

    public DateTime BeginUtc { get; set; }

    public bool UseHtml { get; set; }

    public string? IpAddress { get; set; }
}
