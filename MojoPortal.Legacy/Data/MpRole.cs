using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpRole
{
    public int RoleId { get; set; }

    public int SiteId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? DisplayName { get; set; }

    public Guid? SiteGuid { get; set; }

    public Guid? RoleGuid { get; set; }

    public string? Description { get; set; }

    public virtual MpSite Site { get; set; } = null!;
}
