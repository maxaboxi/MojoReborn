using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpCurrency
{
    public Guid Guid { get; set; }

    public string Title { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? SymbolLeft { get; set; }

    public string? SymbolRight { get; set; }

    public string? DecimalPointChar { get; set; }

    public string? ThousandsPointChar { get; set; }

    public string? DecimalPlaces { get; set; }

    public decimal? Value { get; set; }

    public DateTime? LastModified { get; set; }

    public DateTime Created { get; set; }
}
