namespace Mojo.Modules.Blog.Data;

public class Category
{
    public int Id { get; set; }

    public int ModuleId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();}
