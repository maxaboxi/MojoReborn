using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSharedFilesHistory
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public int ModuleId { get; set; }

    public string FriendlyName { get; set; } = null!;

    public string? OriginalFileName { get; set; }

    public string ServerFileName { get; set; } = null!;

    public int SizeInKb { get; set; }

    public DateTime UploadDate { get; set; }

    public DateTime ArchiveDate { get; set; }

    public int UploadUserId { get; set; }

    public Guid? ItemGuid { get; set; }

    public Guid? ModuleGuid { get; set; }

    public Guid? UserGuid { get; set; }

    public string ViewRoles { get; set; } = null!;

    public virtual MpSharedFile Item { get; set; } = null!;
}
