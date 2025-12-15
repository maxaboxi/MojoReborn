namespace Mojo.Modules.Forum.Domain.Entities;

public class ForumUser
{
    public int Id { get; set; }
    public Guid UserGuid { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public virtual ICollection<ForumThread> Threads { get; set; } = [];
    public virtual ICollection<ForumPost> Posts { get; set; } = [];
}