namespace Mojo.Modules.Forum.Domain.Entities;

public class ForumPostReplyLink
{
    public Guid PostId { get; set; }
    public virtual ForumPost Post { get; set; }
    
    public Guid ParentPostId { get; set; }
    public virtual ForumPost ParentPost { get; set; }
}