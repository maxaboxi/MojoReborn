namespace Mojo.Modules.Blog.Domain.Entities;

public class BlogComment
{
    public Guid Id { get; set; }
    
    public Guid SiteGuid { get; set; }
    
    public Guid FeatureGuid { get; set; }

    public Guid ModuleGuid { get; set; }

    public Guid ContentGuid { get; set; }

    public Guid? UserGuid { get; set; }
    
    public string UserIpAddress { get; set; }

    public string Title { get; set; } 

    public string Content { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string? UserEmail { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public byte ModerationStatus { get; set; }

    public Guid ModeratedBy { get; set; }

    public string? ModerationReason { get; set; }
    
    public BlogPost BlogPost { get; set; }
}
