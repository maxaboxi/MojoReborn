using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpBlogCategory
{
    public int CategoryId { get; set; }

    public int ModuleId { get; set; }

    public string Category { get; set; } = null!;

    public virtual ICollection<MpBlogItemCategory> MpBlogItemCategories { get; set; } = new List<MpBlogItemCategory>();
}
