using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpModule
{
    public int ModuleId { get; set; }

    public int? SiteId { get; set; }

    public int ModuleDefId { get; set; }

    public string? ModuleTitle { get; set; }

    public string? AuthorizedEditRoles { get; set; }

    public int CacheTime { get; set; }

    public bool? ShowTitle { get; set; }

    public int EditUserId { get; set; }

    public bool AvailableForMyPage { get; set; }

    public bool AllowMultipleInstancesOnMyPage { get; set; }

    public string? Icon { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int CountOfUseOnMyPage { get; set; }

    public Guid? Guid { get; set; }

    public Guid? FeatureGuid { get; set; }

    public Guid? SiteGuid { get; set; }

    public Guid? EditUserGuid { get; set; }

    public bool HideFromUnAuth { get; set; }

    public bool HideFromAuth { get; set; }

    public string? ViewRoles { get; set; }

    public string? DraftEditRoles { get; set; }

    public bool? IncludeInSearch { get; set; }

    public bool? IsGlobal { get; set; }

    public string? HeadElement { get; set; }

    public int? PublishMode { get; set; }

    public string? DraftApprovalRoles { get; set; }

    public virtual MpModuleDefinition ModuleDef { get; set; } = null!;

    public virtual ICollection<MpHtmlContent> MpHtmlContents { get; set; } = new List<MpHtmlContent>();

    public virtual ICollection<MpLink> MpLinks { get; set; } = new List<MpLink>();

    public virtual ICollection<MpModuleSetting> MpModuleSettings { get; set; } = new List<MpModuleSetting>();

    public virtual ICollection<MpPageModule> MpPageModules { get; set; } = new List<MpPageModule>();
}
