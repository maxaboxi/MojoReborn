using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpTaxRateHistory
{
    public Guid Guid { get; set; }

    public Guid TaxRateGuid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid GeoZoneGuid { get; set; }

    public Guid TaxClassGuid { get; set; }

    public int Priority { get; set; }

    public decimal Rate { get; set; }

    public string? Description { get; set; }

    public DateTime Created { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime LogTime { get; set; }
}
