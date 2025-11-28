using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpMediaPlayer
{
    public int PlayerId { get; set; }

    public int ModuleId { get; set; }

    public string PlayerType { get; set; } = null!;

    public string Skin { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public Guid? UserGuid { get; set; }

    public Guid? ModuleGuid { get; set; }

    public virtual ICollection<MpMediaTrack> MpMediaTracks { get; set; } = new List<MpMediaTrack>();
}
