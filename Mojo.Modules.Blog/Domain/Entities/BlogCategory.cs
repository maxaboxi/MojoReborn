namespace Mojo.Modules.Blog.Domain.Entities;

public class BlogCategory
{
    public int Id { get; set; }

    public int ModuleId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    
}
