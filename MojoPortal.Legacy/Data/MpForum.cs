using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpForum
{
    public int ItemId { get; set; }

    public int ModuleId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsModerated { get; set; }

    public bool IsActive { get; set; }

    public int SortOrder { get; set; }

    public int ThreadCount { get; set; }

    public int PostCount { get; set; }

    public DateTime? MostRecentPostDate { get; set; }

    public int MostRecentPostUserId { get; set; }

    public int PostsPerPage { get; set; }

    public int ThreadsPerPage { get; set; }

    public bool AllowAnonymousPosts { get; set; }

    public Guid ForumGuid { get; set; }

    public string RolesThatCanPost { get; set; } = null!;

    public string? RolesThatCanModerate { get; set; }

    public string? ModeratorNotifyEmail { get; set; }

    public bool IncludeInGoogleMap { get; set; }

    public bool AddNoIndexMeta { get; set; }

    public bool Closed { get; set; }

    public bool Visible { get; set; }

    public bool RequireModeration { get; set; }

    public bool RequireModForNotify { get; set; }

    public bool AllowTrustedDirectPosts { get; set; }

    public bool AllowTrustedDirectNotify { get; set; }

    public virtual ICollection<MpForumThread> MpForumThreads { get; set; } = new List<MpForumThread>();
}
