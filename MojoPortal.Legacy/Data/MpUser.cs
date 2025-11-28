using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpUser
{
    public int UserId { get; set; }

    public int SiteId { get; set; }

    public string Name { get; set; } = null!;

    public string? LoginName { get; set; }

    public string Email { get; set; } = null!;

    public string? LoweredEmail { get; set; }

    public string? PasswordQuestion { get; set; }

    public string? PasswordAnswer { get; set; }

    public string? Gender { get; set; }

    public bool ProfileApproved { get; set; }

    public Guid? RegisterConfirmGuid { get; set; }

    public bool ApprovedForForums { get; set; }

    public bool Trusted { get; set; }

    public bool? DisplayInMemberList { get; set; }

    public string? WebSiteUrl { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? Occupation { get; set; }

    public string? Interests { get; set; }

    public string? Msn { get; set; }

    public string? Yahoo { get; set; }

    public string? Aim { get; set; }

    public string? Icq { get; set; }

    public int TotalPosts { get; set; }

    public string? AvatarUrl { get; set; }

    public int TimeOffsetHours { get; set; }

    public string? Signature { get; set; }

    public DateTime DateCreated { get; set; }

    public Guid? UserGuid { get; set; }

    public string? Skin { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? LastActivityDate { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public DateTime? LastPasswordChangedDate { get; set; }

    public DateTime? LastLockoutDate { get; set; }

    public int? FailedPasswordAttemptCount { get; set; }

    public DateTime? FailedPwdAttemptWindowStart { get; set; }

    public int? FailedPwdAnswerAttemptCount { get; set; }

    public DateTime? FailedPwdAnswerWindowStart { get; set; }

    public bool IsLockedOut { get; set; }

    public string? MobilePin { get; set; }

    public string? PasswordSalt { get; set; }

    public string? Comment { get; set; }

    public string? OpenIduri { get; set; }

    public string? WindowsLiveId { get; set; }

    public Guid? SiteGuid { get; set; }

    public decimal? TotalRevenue { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Pwd { get; set; }

    public bool? MustChangePwd { get; set; }

    public string? NewEmail { get; set; }

    public string? EditorPreference { get; set; }

    public Guid? EmailChangeGuid { get; set; }

    public string? TimeZoneId { get; set; }

    public Guid? PasswordResetGuid { get; set; }

    public bool? RolesChanged { get; set; }

    public string? AuthorBio { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public int PwdFormat { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTime? LockoutEndDateUtc { get; set; }
}
