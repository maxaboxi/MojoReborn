namespace Mojo.Modules.Forum.Data;

public partial class ForumPost
{
    public int PostId { get; set; }

    public int ThreadId { get; set; }

    public int ThreadSequence { get; set; }

    public string Subject { get; set; } = null!;

    public DateTime PostDate { get; set; }

    public bool Approved { get; set; }

    public int UserId { get; set; }

    public int SortOrder { get; set; }

    public string? Post { get; set; }

    public Guid PostGuid { get; set; }

    public int AnswerVotes { get; set; }

    public Guid ApprovedBy { get; set; }

    public DateTime? ApprovedUtc { get; set; }

    public string? UserIp { get; set; }

    public bool NotificationSent { get; set; }

    public int ModStatus { get; set; }

    public virtual ForumThread Thread { get; set; } = null!;
}
