namespace Mojo.Modules.Forum.Domain.Entities;

public class ForumThread
{
    public int Id { get; set; }

    public int ForumId { get; set; }

    public string ThreadSubject { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int TotalViews { get; set; }

    public int TotalReplies { get; set; }

    public int SortOrder { get; set; }

    public bool IsLocked { get; set; }

    public int ForumSequence { get; set; }

    public DateTime? MostRecentPostDate { get; set; }

    public int? MostRecentPostUserId { get; set; }

    public int StartedByUserId { get; set; }

    public Guid ThreadGuid { get; set; }

    public int ModStatus { get; set; }

    public Guid AssignedTo { get; set; }

    public Guid LockedBy { get; set; }

    public string? LockedReason { get; set; }

    public DateTime? LockedUtc { get; set; }

    public virtual ForumEntity Forum { get; set; } = null!;

    public virtual ICollection<ForumPost> ForumPosts { get; set; } = new List<ForumPost>();
}
