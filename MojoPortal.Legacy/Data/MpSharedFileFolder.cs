using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSharedFileFolder
{
    public int FolderId { get; set; }

    public int ModuleId { get; set; }

    public string FolderName { get; set; } = null!;

    public int ParentId { get; set; }

    public Guid? ModuleGuid { get; set; }

    public Guid? FolderGuid { get; set; }

    public Guid? ParentGuid { get; set; }

    public string ViewRoles { get; set; } = null!;
}
