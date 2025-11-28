using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpRssFeedEntry
{
    public Guid RowGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public Guid FeedGuid { get; set; }

    public int FeedId { get; set; }

    public DateTime PubDate { get; set; }

    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public string BlogUrl { get; set; } = null!;

    public string? Description { get; set; }

    public string Link { get; set; } = null!;

    public bool Confirmed { get; set; }

    public int EntryHash { get; set; }

    public DateTime CachedTimeUtc { get; set; }
}
