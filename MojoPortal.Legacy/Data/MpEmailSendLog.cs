using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpEmailSendLog
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public Guid SpecialGuid1 { get; set; }

    public Guid SpecialGuid2 { get; set; }

    public string ToAddress { get; set; } = null!;

    public string? CcAddress { get; set; }

    public string? BccAddress { get; set; }

    public string Subject { get; set; } = null!;

    public string? TextBody { get; set; }

    public string? HtmlBody { get; set; }

    public string Type { get; set; } = null!;

    public DateTime SentUtc { get; set; }

    public string? FromAddress { get; set; }

    public string? ReplyTo { get; set; }

    public Guid? UserGuid { get; set; }
}
