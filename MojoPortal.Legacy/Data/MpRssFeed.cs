using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpRssFeed
{
    public int ItemId { get; set; }

    public int ModuleId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UserId { get; set; }

    public string Author { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string RssUrl { get; set; } = null!;

    public Guid? ItemGuid { get; set; }

    public Guid? ModuleGuid { get; set; }

    public Guid? UserGuid { get; set; }

    public Guid? LastModUserGuid { get; set; }

    public DateTime? LastModUtc { get; set; }

    public string? ImageUrl { get; set; }

    public string? FeedType { get; set; }

    public bool? PublishByDefault { get; set; }

    public int? SortRank { get; set; }
}
