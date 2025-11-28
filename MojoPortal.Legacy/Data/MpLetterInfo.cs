using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpLetterInfo
{
    public Guid LetterInfoGuid { get; set; }

    public Guid SiteGuid { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string AvailableToRoles { get; set; } = null!;

    public bool Enabled { get; set; }

    public bool AllowUserFeedback { get; set; }

    public bool AllowAnonFeedback { get; set; }

    public string FromAddress { get; set; } = null!;

    public string FromName { get; set; } = null!;

    public string ReplyToAddress { get; set; } = null!;

    public int SendMode { get; set; }

    public bool EnableViewAsWebPage { get; set; }

    public bool EnableSendLog { get; set; }

    public string? RolesThatCanEdit { get; set; }

    public string? RolesThatCanApprove { get; set; }

    public string? RolesThatCanSend { get; set; }

    public int SubscriberCount { get; set; }

    public DateTime CreatedUtc { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime LastModUtc { get; set; }

    public Guid LastModBy { get; set; }

    public bool? AllowArchiveView { get; set; }

    public bool? ProfileOptIn { get; set; }

    public int? SortRank { get; set; }

    public int? UnVerifiedCount { get; set; }

    public string? DisplayNameDefault { get; set; }

    public string? FirstNameDefault { get; set; }

    public string? LastNameDefault { get; set; }

    public virtual ICollection<MpLetter> MpLetters { get; set; } = new List<MpLetter>();
}
