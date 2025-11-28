using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpCommerceReportOrder
{
    public Guid RowGuid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid FeatureGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public Guid UserGuid { get; set; }

    public Guid OrderGuid { get; set; }

    public string? BillingFirstName { get; set; }

    public string? BillingLastName { get; set; }

    public string? BillingCompany { get; set; }

    public string? BillingAddress1 { get; set; }

    public string? BillingAddress2 { get; set; }

    public string? BillingSuburb { get; set; }

    public string? BillingCity { get; set; }

    public string? BillingPostalCode { get; set; }

    public string? BillingState { get; set; }

    public string? BillingCountry { get; set; }

    public string? PaymentMethod { get; set; }

    public decimal SubTotal { get; set; }

    public decimal TaxTotal { get; set; }

    public decimal ShippingTotal { get; set; }

    public decimal OrderTotal { get; set; }

    public DateTime OrderDateUtc { get; set; }

    public string AdminOrderLink { get; set; } = null!;

    public string UserOrderLink { get; set; } = null!;

    public DateTime RowCreatedUtc { get; set; }

    public bool IncludeInAggregate { get; set; }
}
