using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpContactFormMessage
{
    public Guid RowGuid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public string Email { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime CreatedUtc { get; set; }

    public string CreatedFromIpAddress { get; set; } = null!;

    public Guid UserGuid { get; set; }
}
