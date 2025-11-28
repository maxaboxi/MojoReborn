using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSystemLog
{
    public int Id { get; set; }

    public DateTime LogDate { get; set; }

    public string? IpAddress { get; set; }

    public string? Culture { get; set; }

    public string? Url { get; set; }

    public string? ShortUrl { get; set; }

    public string Thread { get; set; } = null!;

    public string LogLevel { get; set; } = null!;

    public string Logger { get; set; } = null!;

    public string Message { get; set; } = null!;
}
