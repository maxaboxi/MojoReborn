using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpHtmlContent
{
    public int ItemId { get; set; }

    public int ModuleId { get; set; }

    public string? Title { get; set; }

    public string? Excerpt { get; set; }

    public string? Body { get; set; }

    public string? MoreLink { get; set; }

    public int SortOrder { get; set; }

    public DateTime BeginDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UserId { get; set; }

    public Guid? ItemGuid { get; set; }

    public Guid? ModuleGuid { get; set; }

    public Guid? UserGuid { get; set; }

    public Guid? LastModUserGuid { get; set; }

    public DateTime? LastModUtc { get; set; }

    public bool ExcludeFromRecentContent { get; set; }

    public virtual MpModule Module { get; set; } = null!;
}
