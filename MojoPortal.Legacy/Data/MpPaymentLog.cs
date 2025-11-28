using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpPaymentLog
{
    public Guid RowGuid { get; set; }

    public DateTime CreatedUtc { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid UserGuid { get; set; }

    public Guid StoreGuid { get; set; }

    public Guid CartGuid { get; set; }

    public string? Provider { get; set; }

    public string? RawResponse { get; set; }

    public string? ResponseCode { get; set; }

    public string? ResponseReasonCode { get; set; }

    public string? Reason { get; set; }

    public string? AvsCode { get; set; }

    public string? CcvCode { get; set; }

    public string? CavCode { get; set; }

    public string? TransactionId { get; set; }

    public string? TransactionType { get; set; }

    public string? Method { get; set; }

    public string? AuthCode { get; set; }

    public decimal? Amount { get; set; }

    public decimal? Tax { get; set; }

    public decimal? Duty { get; set; }

    public decimal? Freight { get; set; }
}
