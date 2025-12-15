namespace Mojo.Modules.Forum.Features.Threads.GetThread;

public record ForumPostDto(
    int ForumId, 
    int Id,
    Guid PostGuid,
    int ThreadId, 
    int ThreadSequence, 
    string ThreadSubject,
    string Content,
    int Points,
    int UserId,
    string UserName,
    Guid? ReplyToPostId,
    DateTime CreatedAt);
