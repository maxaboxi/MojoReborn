using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpLetterHtmlTemplate
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public string Title { get; set; } = null!;

    public string? Html { get; set; }

    public DateTime LastModUtc { get; set; }
}
