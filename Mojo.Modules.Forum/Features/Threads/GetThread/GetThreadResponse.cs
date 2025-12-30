namespace Mojo.Modules.Forum.Features.Threads.GetThread;

public record GetThreadResponse(int Id, int ForumId, string Subject, Guid ThreadGuid, int UserId, string UserName, List<ForumPostDto> ForumPosts);