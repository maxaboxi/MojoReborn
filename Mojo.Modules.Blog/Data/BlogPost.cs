namespace Mojo.Modules.Blog.Data;

public class BlogPost
{
    public int Id { get; set; }
    
    public Guid BlogPostId { get; set; }

    public int ModuleId { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }
    
    public string SubTitle { get; set; }
    
    public string Slug { get; set; }
    
    public string Author { get; set; }
    public Guid LastModifiedBy { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt {get; set; }
    
    public bool IsPublished { get; set; }
    
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    public virtual ICollection<BlogComment> Comments { get; set; } = new List<BlogComment>();

}
