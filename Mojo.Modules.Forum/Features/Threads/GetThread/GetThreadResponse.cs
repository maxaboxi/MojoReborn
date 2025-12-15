using Mojo.Shared.Responses;

namespace Mojo.Modules.Forum.Features.Threads.GetThread;

public class GetThreadResponse : BaseResponse
{
    public int Id { get; set; }
    public int ForumId { get; set; }
    public string Subject  { get; set; }
    public Guid ThreadGuid  { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public List<ForumPostDto> ForumPosts { get; set; } = [];
}