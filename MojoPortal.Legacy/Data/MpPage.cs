using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpPage
{
    public int PageId { get; set; }

    public int? ParentId { get; set; }

    public int PageOrder { get; set; }

    public int SiteId { get; set; }

    public string? PageName { get; set; }

    public string? PageTitle { get; set; }

    public string? AuthorizedRoles { get; set; }

    public string? EditRoles { get; set; }

    public string? CreateChildPageRoles { get; set; }

    public bool RequireSsl { get; set; }

    public bool AllowBrowserCache { get; set; }

    public bool ShowBreadcrumbs { get; set; }

    public string? PageKeyWords { get; set; }

    public string? PageDescription { get; set; }

    public string? PageEncoding { get; set; }

    public string? AdditionalMetaTags { get; set; }

    public string? MenuImage { get; set; }

    public bool UseUrl { get; set; }

    public string? Url { get; set; }

    public bool OpenInNewWindow { get; set; }

    public bool ShowChildPageMenu { get; set; }

    public bool ShowChildBreadCrumbs { get; set; }

    public string? Skin { get; set; }

    public bool HideMainMenu { get; set; }

    public bool IncludeInMenu { get; set; }

    public string? ChangeFrequency { get; set; }

    public string? SiteMapPriority { get; set; }

    public DateTime? LastModifiedUtc { get; set; }

    public Guid PageGuid { get; set; }

    public Guid ParentGuid { get; set; }

    public bool HideAfterLogin { get; set; }

    public Guid? SiteGuid { get; set; }

    public string? CompiledMeta { get; set; }

    public DateTime? CompiledMetaUtc { get; set; }

    public bool? IncludeInSiteMap { get; set; }

    public bool? IsClickable { get; set; }

    public bool? ShowHomeCrumb { get; set; }

    public string? DraftEditRoles { get; set; }

    public bool IsPending { get; set; }

    public string? CanonicalOverride { get; set; }

    public bool? IncludeInSearchMap { get; set; }

    public bool? EnableComments { get; set; }

    public string? CreateChildDraftRoles { get; set; }

    public bool? IncludeInChildSiteMap { get; set; }

    public Guid? PubTeamId { get; set; }

    public string? BodyCssClass { get; set; }

    public string? MenuCssClass { get; set; }

    public bool? ExpandOnSiteMap { get; set; }

    public int? PublishMode { get; set; }

    public DateTime PcreatedUtc { get; set; }

    public Guid? PcreatedBy { get; set; }

    public string? PcreatedFromIp { get; set; }

    public DateTime PlastModUtc { get; set; }

    public Guid? PlastModBy { get; set; }

    public string? PlastModFromIp { get; set; }

    public string? MenuDesc { get; set; }

    public string? DraftApprovalRoles { get; set; }

    public string? LinkRel { get; set; }

    public string? PageHeading { get; set; }

    public bool ShowPageHeading { get; set; }

    public DateTime PubDateUtc { get; set; }

    public virtual ICollection<MpPageModule> MpPageModules { get; set; } = new List<MpPageModule>();

    public virtual MpSite Site { get; set; } = null!;
}
