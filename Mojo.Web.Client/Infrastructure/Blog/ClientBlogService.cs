using Mojo.Modules.Blog.UI.Services;
using Mojo.Shared.Features.Blog;

namespace Mojo.Web.Client.Infrastructure.Blog;

public class ClientBlogService(IBlogApi api) : IBlogService
{
    public async Task<List<BlogPostDto>> GetPostsAsync()
    {
        return await api.GetPostsAsync();
    }

    public async Task<BlogPostDto> GetPostAsync(Guid id)
    {
        return await api.GetPostAsync(id);
    }
}