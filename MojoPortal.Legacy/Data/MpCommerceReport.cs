using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpCommerceReport
{
    public Guid RowGuid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid UserGuid { get; set; }

    public Guid FeatureGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public string? ModuleTitle { get; set; }

    public Guid OrderGuid { get; set; }

    public Guid ItemGuid { get; set; }

    public string? ItemName { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal SubTotal { get; set; }

    public DateTime OrderDateUtc { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Ipaddress { get; set; }

    public string AdminOrderLink { get; set; } = null!;

    public string UserOrderLink { get; set; } = null!;

    public DateTime RowCreatedUtc { get; set; }

    public bool IncludeInAggregate { get; set; }
}
