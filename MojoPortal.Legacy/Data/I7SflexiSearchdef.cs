using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class I7SflexiSearchdef
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid FeatureGuid { get; set; }

    public Guid FieldDefinitionGuid { get; set; }

    public string Title { get; set; } = null!;

    public string Keywords { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Link { get; set; } = null!;

    public string LinkQueryAddendum { get; set; } = null!;
}
