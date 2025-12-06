using System.ComponentModel.DataAnnotations;

namespace Mojo.Modules.Blog.Domain.Entities;

public class BlogPost
{
    public int Id { get; set; }
    
    public Guid BlogPostId { get; set; }

    public int ModuleId { get; set; }
    
    public Guid ModuleGuid { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    public string SubTitle { get; set; }
    
    public string Slug { get; set; }
    
    public string Author { get; set; }
    public Guid LastModifiedBy { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt {get; set; }
    
    public bool IsPublished { get; set; }

    public virtual ICollection<BlogCategory> Categories { get; set; } = new List<BlogCategory>();
    public virtual ICollection<BlogComment> Comments { get; set; } = new List<BlogComment>();

}
