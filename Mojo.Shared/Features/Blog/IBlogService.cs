namespace Mojo.Shared.Features.Blog;

public interface IBlogService
{
    Task<List<BlogPostDto>> GetPostsAsync();
    Task<BlogPostDto> GetPostAsync(Guid id);
}