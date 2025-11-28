using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpBlogComment
{
    public int BlogCommentId { get; set; }

    public int ModuleId { get; set; }

    public int ItemId { get; set; }

    public string Comment { get; set; } = null!;

    public string? Title { get; set; }

    public string? Name { get; set; }

    public string? Url { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual MpBlog Item { get; set; } = null!;
}
