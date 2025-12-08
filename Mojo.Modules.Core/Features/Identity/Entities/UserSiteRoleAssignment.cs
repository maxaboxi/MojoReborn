namespace Mojo.Modules.Core.Features.Identity.Entities;

public class UserSiteRoleAssignment
{
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    public Guid RoleId { get; set; }
    public virtual SiteRole Role { get; set; }
}