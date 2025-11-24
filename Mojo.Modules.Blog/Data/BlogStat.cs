namespace Mojo.Modules.Blog.Data;

public partial class BlogStat
{
    public int ModuleId { get; set; }

    public int EntryCount { get; set; }

    public int CommentCount { get; set; }

    public int TrackBackCount { get; set; }

    public Guid? ModuleGuid { get; set; }
}
