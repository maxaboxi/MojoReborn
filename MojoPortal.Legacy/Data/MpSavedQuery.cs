using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSavedQuery
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Statement { get; set; }

    public DateTime CreatedUtc { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime LastModUtc { get; set; }

    public Guid LastModBy { get; set; }
}
