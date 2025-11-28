using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpUserRole
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public Guid? UserGuid { get; set; }

    public Guid? RoleGuid { get; set; }
}
