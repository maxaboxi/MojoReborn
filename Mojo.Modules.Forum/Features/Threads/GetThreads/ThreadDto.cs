namespace Mojo.Modules.Forum.Features.Threads.GetThreads;

public record ThreadDto(
    int Id,
    int ForumId,
    string Subject,
    DateTime CreatedAt,
    int TotalViews,
    int TotalReplies,
    int SortOrder,
    bool IsLocked,
    DateTime? MostRecentPostDate,
    int? MostRecentPostUserId,
    int StartedByUserId,
    string StartedByUserName,
    Guid ThreadGuid,
    string? LockedReason,
    DateTime? LockedUtc
    );