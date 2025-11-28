using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpGoogleCheckoutLog
{
    public Guid RowGuid { get; set; }

    public DateTime CreatedUtc { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid UserGuid { get; set; }

    public Guid StoreGuid { get; set; }

    public Guid CartGuid { get; set; }

    public string? NotificationType { get; set; }

    public string? RawResponse { get; set; }

    public string? SerialNumber { get; set; }

    public DateTime? Gtimestamp { get; set; }

    public string? OrderNumber { get; set; }

    public string? BuyerId { get; set; }

    public string? FullfillState { get; set; }

    public string? FinanceState { get; set; }

    public bool EmailListOptIn { get; set; }

    public string? AvsResponse { get; set; }

    public string? CvnResponse { get; set; }

    public DateTime? AuthExpDate { get; set; }

    public decimal AuthAmt { get; set; }

    public decimal DiscountTotal { get; set; }

    public decimal ShippingTotal { get; set; }

    public decimal TaxTotal { get; set; }

    public decimal OrderTotal { get; set; }

    public decimal LatestChgAmt { get; set; }

    public decimal TotalChgAmt { get; set; }

    public decimal LatestRefundAmt { get; set; }

    public decimal TotalRefundAmt { get; set; }

    public decimal LatestChargeback { get; set; }

    public decimal TotalChargeback { get; set; }

    public string? CartXml { get; set; }

    public string? ProviderName { get; set; }
}
