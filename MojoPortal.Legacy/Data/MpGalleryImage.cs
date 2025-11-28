using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpGalleryImage
{
    public int ItemId { get; set; }

    public int ModuleId { get; set; }

    public int DisplayOrder { get; set; }

    public string? Caption { get; set; }

    public string? Description { get; set; }

    public string? MetaDataXml { get; set; }

    public string? ImageFile { get; set; }

    public string? WebImageFile { get; set; }

    public string? ThumbnailFile { get; set; }

    public DateTime UploadDate { get; set; }

    public string? UploadUser { get; set; }

    public Guid? ItemGuid { get; set; }

    public Guid? ModuleGuid { get; set; }

    public Guid? UserGuid { get; set; }
}
