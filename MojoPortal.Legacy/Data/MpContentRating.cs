using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpContentRating
{
    public Guid RowGuid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid ContentGuid { get; set; }

    public Guid UserGuid { get; set; }

    public string? EmailAddress { get; set; }

    public int Rating { get; set; }

    public string? Comments { get; set; }

    public string? IpAddress { get; set; }

    public DateTime CreatedUtc { get; set; }

    public DateTime LastModUtc { get; set; }
}
