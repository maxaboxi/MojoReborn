using Microsoft.AspNetCore.Identity;

namespace Mojo.Modules.Core.Features.Identity.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string? Bio { get; set; }
    public string? Signature { get; set; }
    
    public string? TimeZoneId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginDate { get; set; }
    
    public bool IsDeleted { get; set; }
    public virtual ICollection<UserSiteProfile> UserSiteProfiles { get; set; } = [];
    public virtual ICollection<UserSiteRoleAssignment> UserSiteRoleAssignments { get; set; } = [];
}