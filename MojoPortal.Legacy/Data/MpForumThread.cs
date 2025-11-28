using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpForumThread
{
    public int ThreadId { get; set; }

    public int ForumId { get; set; }

    public string ThreadSubject { get; set; } = null!;

    public DateTime ThreadDate { get; set; }

    public int TotalViews { get; set; }

    public int TotalReplies { get; set; }

    public int SortOrder { get; set; }

    public bool IsLocked { get; set; }

    public int ForumSequence { get; set; }

    public DateTime? MostRecentPostDate { get; set; }

    public int? MostRecentPostUserId { get; set; }

    public int StartedByUserId { get; set; }

    public Guid ThreadGuid { get; set; }

    public bool IsQuestion { get; set; }

    public bool IncludeInSiteMap { get; set; }

    public bool SetNoIndexMeta { get; set; }

    public string? PtitleOverride { get; set; }

    public int ModStatus { get; set; }

    public string? ThreadType { get; set; }

    public Guid AssignedTo { get; set; }

    public Guid LockedBy { get; set; }

    public string? LockedReason { get; set; }

    public DateTime? LockedUtc { get; set; }

    public virtual MpForum Forum { get; set; } = null!;

    public virtual ICollection<MpForumPost> MpForumPosts { get; set; } = new List<MpForumPost>();

    public virtual ICollection<MpForumThreadSubscription> MpForumThreadSubscriptions { get; set; } = new List<MpForumThreadSubscription>();
}
