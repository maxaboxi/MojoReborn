using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpMediaFile
{
    public int FileId { get; set; }

    public int TrackId { get; set; }

    public string FilePath { get; set; } = null!;

    public DateTime AddedDate { get; set; }

    public Guid? UserGuid { get; set; }

    public virtual MpMediaTrack Track { get; set; } = null!;
}
