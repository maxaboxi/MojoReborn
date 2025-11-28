using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpBlogStat
{
    public int ModuleId { get; set; }

    public int EntryCount { get; set; }

    public int CommentCount { get; set; }

    public int TrackBackCount { get; set; }

    public Guid? ModuleGuid { get; set; }
}
