using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpUserProperty
{
    public Guid PropertyId { get; set; }

    public Guid UserGuid { get; set; }

    public string? PropertyName { get; set; }

    public string? PropertyValueString { get; set; }

    public byte[]? PropertyValueBinary { get; set; }

    public DateTime LastUpdatedDate { get; set; }

    public bool IsLazyLoaded { get; set; }
}
