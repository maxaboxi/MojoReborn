using Mojo.Modules.Blog.Features.GetPost;
using Mojo.Modules.Blog.Features.GetPosts;
using Mojo.Shared.Features.Blog;
using Wolverine;

namespace Mojo.Web.Infrastructure.Blog;

public class ServerBlogService(IMessageBus bus) : IBlogService
{

    public async Task<List<BlogPostDto>> GetPostsAsync()
    {
        return await bus.InvokeAsync<List<BlogPostDto>>(new GetPostsQuery());
    }

    public async Task<BlogPostDto> GetPostAsync(Guid id)
    {
        return await bus.InvokeAsync<BlogPostDto>(new GetPostQuery(id));
    }
}