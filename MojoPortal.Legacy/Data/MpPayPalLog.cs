using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpPayPalLog
{
    public Guid RowGuid { get; set; }

    public DateTime CreatedUtc { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid UserGuid { get; set; }

    public Guid StoreGuid { get; set; }

    public Guid CartGuid { get; set; }

    public string RequestType { get; set; } = null!;

    public string? ApiVersion { get; set; }

    public string? RawResponse { get; set; }

    public string? Token { get; set; }

    public string? PayerId { get; set; }

    public string? TransactionId { get; set; }

    public string? PaymentType { get; set; }

    public string? PaymentStatus { get; set; }

    public string? PendingReason { get; set; }

    public string? ReasonCode { get; set; }

    public string? CurrencyCode { get; set; }

    public decimal ExchangeRate { get; set; }

    public decimal CartTotal { get; set; }

    public decimal PayPalAmt { get; set; }

    public decimal TaxAmt { get; set; }

    public decimal FeeAmt { get; set; }

    public decimal SettleAmt { get; set; }

    public string? ProviderName { get; set; }

    public string? ReturnUrl { get; set; }

    public string? SerializedObject { get; set; }

    public string? PdtproviderName { get; set; }

    public string? IpnproviderName { get; set; }

    public string? Response { get; set; }
}
