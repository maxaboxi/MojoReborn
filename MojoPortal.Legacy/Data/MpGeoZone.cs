using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpGeoZone
{
    public Guid Guid { get; set; }

    public Guid CountryGuid { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual MpGeoCountry Country { get; set; } = null!;
}
