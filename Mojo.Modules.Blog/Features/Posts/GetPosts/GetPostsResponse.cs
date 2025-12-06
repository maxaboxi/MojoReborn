using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.Posts.GetPosts;

public class GetPostsResponse : BaseResponse
{
    public List<BlogPostDto> BlogPosts { get; set; } = [];
}