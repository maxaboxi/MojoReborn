using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpUserPage
{
    public Guid UserPageId { get; set; }

    public int SiteId { get; set; }

    public Guid UserGuid { get; set; }

    public string PageName { get; set; } = null!;

    public string PagePath { get; set; } = null!;

    public int PageOrder { get; set; }

    public Guid? SiteGuid { get; set; }
}
