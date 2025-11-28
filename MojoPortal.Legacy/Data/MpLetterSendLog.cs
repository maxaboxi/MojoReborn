using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpLetterSendLog
{
    public int RowId { get; set; }

    public Guid LetterGuid { get; set; }

    public Guid UserGuid { get; set; }

    public string? EmailAddress { get; set; }

    public DateTime Utc { get; set; }

    public bool ErrorOccurred { get; set; }

    public string? ErrorMessage { get; set; }

    public Guid? SubscribeGuid { get; set; }
}
