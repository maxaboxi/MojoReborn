namespace Mojo.Modules.Blog.Features.Posts.CreatePost;

public class CreatePostCategoryDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;
}