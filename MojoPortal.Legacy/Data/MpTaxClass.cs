using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpTaxClass
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? LastModified { get; set; }

    public DateTime Created { get; set; }
}
