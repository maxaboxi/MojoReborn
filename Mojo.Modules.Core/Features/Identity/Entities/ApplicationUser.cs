using Microsoft.AspNetCore.Identity;

namespace Mojo.Modules.Core.Features.Identity.Entities;

public class ApplicationUser : IdentityUser
{
    public int SiteId { get; set; }
    public Guid SiteGuid { get; set; }
    
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
}