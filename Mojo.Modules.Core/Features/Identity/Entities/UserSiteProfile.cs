namespace Mojo.Modules.Core.Features.Identity.Entities;

public class UserSiteProfile
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    public int SiteId { get; set; }
    public Guid SiteGuid { get; set; }
}