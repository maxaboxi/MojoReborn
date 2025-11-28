using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSharedFile
{
    public int ItemId { get; set; }

    public int ModuleId { get; set; }

    public int UploadUserId { get; set; }

    public string FriendlyName { get; set; } = null!;

    public string OriginalFileName { get; set; } = null!;

    public string ServerFileName { get; set; } = null!;

    public int SizeInKb { get; set; }

    public DateTime UploadDate { get; set; }

    public int FolderId { get; set; }

    public Guid? ItemGuid { get; set; }

    public Guid? ModuleGuid { get; set; }

    public Guid? UserGuid { get; set; }

    public byte[]? FileBlob { get; set; }

    public Guid? FolderGuid { get; set; }

    public string? Description { get; set; }

    public int? DownloadCount { get; set; }

    public string ViewRoles { get; set; } = null!;

    public virtual ICollection<MpSharedFilesHistory> MpSharedFilesHistories { get; set; } = new List<MpSharedFilesHistory>();
}
