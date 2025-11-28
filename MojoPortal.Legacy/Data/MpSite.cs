using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSite
{
    public int SiteId { get; set; }

    public Guid SiteGuid { get; set; }

    public string? SiteAlias { get; set; }

    public string SiteName { get; set; } = null!;

    public string? Skin { get; set; }

    public string? Logo { get; set; }

    public string? Icon { get; set; }

    public bool AllowUserSkins { get; set; }

    public bool AllowPageSkins { get; set; }

    public bool AllowHideMenuOnPages { get; set; }

    public bool AllowNewRegistration { get; set; }

    public bool UseSecureRegistration { get; set; }

    public bool UseSslonAllPages { get; set; }

    public string? DefaultPageKeyWords { get; set; }

    public string? DefaultPageDescription { get; set; }

    public string? DefaultPageEncoding { get; set; }

    public string? DefaultAdditionalMetaTags { get; set; }

    public bool IsServerAdminSite { get; set; }

    public bool UseLdapAuth { get; set; }

    public bool AutoCreateLdapUserOnFirstLogin { get; set; }

    public string? LdapServer { get; set; }

    public int LdapPort { get; set; }

    public string? LdapDomain { get; set; }

    public string? LdapRootDn { get; set; }

    public string LdapUserDnkey { get; set; } = null!;

    public bool ReallyDeleteUsers { get; set; }

    public bool UseEmailForLogin { get; set; }

    public bool AllowUserFullNameChange { get; set; }

    public string EditorSkin { get; set; } = null!;

    public string DefaultFriendlyUrlPatternEnum { get; set; } = null!;

    public bool AllowPasswordRetrieval { get; set; }

    public bool AllowPasswordReset { get; set; }

    public bool RequiresQuestionAndAnswer { get; set; }

    public int MaxInvalidPasswordAttempts { get; set; }

    public int PasswordAttemptWindowMinutes { get; set; }

    public bool RequiresUniqueEmail { get; set; }

    public int PasswordFormat { get; set; }

    public int MinRequiredPasswordLength { get; set; }

    public int MinReqNonAlphaChars { get; set; }

    public string? PwdStrengthRegex { get; set; }

    public string? DefaultEmailFromAddress { get; set; }

    public bool EnableMyPageFeature { get; set; }

    public string? EditorProvider { get; set; }

    public string? CaptchaProvider { get; set; }

    public string? DatePickerProvider { get; set; }

    public string? RecaptchaPrivateKey { get; set; }

    public string? RecaptchaPublicKey { get; set; }

    public string? WordpressApikey { get; set; }

    public string? WindowsLiveAppId { get; set; }

    public string? WindowsLiveKey { get; set; }

    public bool AllowOpenIdauth { get; set; }

    public bool AllowWindowsLiveAuth { get; set; }

    public string? GmapApiKey { get; set; }

    public string? ApiKeyExtra1 { get; set; }

    public string? ApiKeyExtra2 { get; set; }

    public string? ApiKeyExtra3 { get; set; }

    public string? ApiKeyExtra4 { get; set; }

    public string? ApiKeyExtra5 { get; set; }

    public bool? DisableDbAuth { get; set; }

    public virtual ICollection<MpPage> MpPages { get; set; } = new List<MpPage>();

    public virtual ICollection<MpRole> MpRoles { get; set; } = new List<MpRole>();
}
