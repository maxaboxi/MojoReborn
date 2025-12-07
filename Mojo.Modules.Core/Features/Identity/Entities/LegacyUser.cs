namespace Mojo.Modules.Core.Features.Identity.Entities;

public class LegacyUser
{
    public Guid UserGuid { get; set; }
    public int UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Pwd { get; set; } = string.Empty; // Plain text password
    public string? PasswordHash { get; set; } = string.Empty;
    public string? PasswordSalt { get; set; }
    public int PwdFormat { get; set; } // 0=Clear, 1=Hashed
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public int SiteId { get; set; }
    public Guid SiteGuid { get; set; }
}