using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpEmailSendQueue
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public Guid UserGuid { get; set; }

    public Guid SpecialGuid1 { get; set; }

    public Guid SpecialGuid2 { get; set; }

    public string FromAddress { get; set; } = null!;

    public string ReplyTo { get; set; } = null!;

    public string ToAddress { get; set; } = null!;

    public string? CcAddress { get; set; }

    public string? BccAddress { get; set; }

    public string Subject { get; set; } = null!;

    public string? TextBody { get; set; }

    public string? HtmlBody { get; set; }

    public string Type { get; set; } = null!;

    public DateTime DateToSend { get; set; }

    public DateTime CreatedUtc { get; set; }
}
