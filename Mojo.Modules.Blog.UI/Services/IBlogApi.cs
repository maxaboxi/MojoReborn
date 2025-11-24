using Mojo.Shared.Features.Blog;
using Refit;

namespace Mojo.Modules.Blog.UI.Services;

public interface IBlogApi
{
    [Get("/api/blog/posts")]
    Task<List<BlogPostDto>> GetPostsAsync();
    
    [Get("/api/blog/posts/{id}")]
    Task<BlogPostDto> GetPostAsync(Guid id);
}