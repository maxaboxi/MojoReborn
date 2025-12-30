namespace Mojo.Modules.Blog.Domain.Entities;

public class BlogSubscription
{
    public Guid Id { get; set; }
    public int SiteId { get; set; }
    public Guid SiteGuid { get; set; }
    public int ModuleId { get; set; }
    public Guid ModuleGuid { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}