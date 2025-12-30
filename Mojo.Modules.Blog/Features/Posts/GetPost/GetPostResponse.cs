namespace Mojo.Modules.Blog.Features.Posts.GetPost;

public record GetPostResponse(
    Guid BlogPostId, 
    string Title, 
    string SubTitle, 
    string Content, 
    string Author, 
    DateTime CreatedAt,
    List<string> Categories,
    int CommentCount,
    List<BlogCommentDto> Comments);