using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpLanguage
{
    public Guid Guid { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int Sort { get; set; }
}
