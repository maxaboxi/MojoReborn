using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpBlogItemCategory
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public int CategoryId { get; set; }

    public virtual MpBlogCategory Category { get; set; } = null!;

    public virtual MpBlog Item { get; set; } = null!;
}
