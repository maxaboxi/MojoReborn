namespace Mojo.Modules.Core.Features.Identity.Entities;

public class SiteRole
{
    public Guid Id { get; set; }

    public int SiteId { get; set; }
    public Guid? SiteGuid { get; set; }

    public string Name { get; set; } = null!;
    public string DisplayName { get; set; }
    public string? Description { get; set; }

    public virtual ICollection<UserSiteRoleAssignment> UserSiteRoleAssignments { get; set; } = [];

}