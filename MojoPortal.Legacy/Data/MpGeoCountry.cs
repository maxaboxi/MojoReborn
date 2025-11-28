using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpGeoCountry
{
    public Guid Guid { get; set; }

    public string Name { get; set; } = null!;

    public string Isocode2 { get; set; } = null!;

    public string Isocode3 { get; set; } = null!;

    public virtual ICollection<MpGeoZone> MpGeoZones { get; set; } = new List<MpGeoZone>();
}
