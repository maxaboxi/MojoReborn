using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpBlog
{
    public int ItemId { get; set; }

    public int ModuleId { get; set; }

    public string? CreatedByUser { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Title { get; set; }

    public DateTime? StartDate { get; set; }

    public bool? IsInNewsletter { get; set; }

    public string? Description { get; set; }

    public int CommentCount { get; set; }

    public int TrackBackCount { get; set; }

    public string? Categories { get; set; }

    public bool IncludeInFeed { get; set; }

    public int AllowCommentsForDays { get; set; }

    public Guid? BlogGuid { get; set; }

    public Guid? ModuleGuid { get; set; }

    public string? Location { get; set; }

    public Guid? UserGuid { get; set; }

    public Guid? LastModUserGuid { get; set; }

    public DateTime? LastModUtc { get; set; }

    public string? ItemUrl { get; set; }

    public string? Heading { get; set; }

    public string? MetaKeywords { get; set; }

    public string? MetaDescription { get; set; }

    public string? Abstract { get; set; }

    public string? CompiledMeta { get; set; }

    public bool? IsPublished { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? Approved { get; set; }

    public Guid? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public string? SubTitle { get; set; }

    public bool ShowAuthorName { get; set; }

    public bool ShowAuthorAvatar { get; set; }

    public bool ShowAuthorBio { get; set; }

    public bool IncludeInSearch { get; set; }

    public bool IncludeInSiteMap { get; set; }

    public bool UseBingMap { get; set; }

    public string MapHeight { get; set; } = null!;

    public string MapWidth { get; set; } = null!;

    public bool ShowMapOptions { get; set; }

    public bool ShowZoomTool { get; set; }

    public bool ShowLocationInfo { get; set; }

    public bool UseDrivingDirections { get; set; }

    public string MapType { get; set; } = null!;

    public int MapZoom { get; set; }

    public bool ShowDownloadLink { get; set; }

    public bool ExcludeFromRecentContent { get; set; }

    public bool IncludeInNews { get; set; }

    public string? PubName { get; set; }

    public string? PubLanguage { get; set; }

    public string? PubAccess { get; set; }

    public string? PubGenres { get; set; }

    public string? PubKeyWords { get; set; }

    public string? PubGeoLocations { get; set; }

    public string? PubStockTickers { get; set; }

    public string? HeadlineImageUrl { get; set; }

    public bool IncludeImageInExcerpt { get; set; }

    public bool IncludeImageInPost { get; set; }

    public virtual ICollection<MpBlogComment> MpBlogComments { get; set; } = new List<MpBlogComment>();

    public virtual ICollection<MpBlogItemCategory> MpBlogItemCategories { get; set; } = new List<MpBlogItemCategory>();
}
