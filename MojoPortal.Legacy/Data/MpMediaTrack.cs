using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpMediaTrack
{
    public int TrackId { get; set; }

    public int PlayerId { get; set; }

    public string TrackType { get; set; } = null!;

    public int TrackOrder { get; set; }

    public string Name { get; set; } = null!;

    public string? Artist { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? UserGuid { get; set; }

    public virtual ICollection<MpMediaFile> MpMediaFiles { get; set; } = new List<MpMediaFile>();

    public virtual MpMediaPlayer Player { get; set; } = null!;
}
