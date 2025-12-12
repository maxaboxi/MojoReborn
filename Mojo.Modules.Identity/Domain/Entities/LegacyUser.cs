namespace Mojo.Modules.Identity.Domain.Entities;

public class LegacyUser
{
    public Guid UserGuid { get; set; }
    public int UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; }
    public string? Pwd { get; set; } = string.Empty; // Plain text password
    public string? PasswordHash { get; set; } = string.Empty;
    public string? PasswordSalt { get; set; }
    public int PwdFormat { get; set; } // 0=Clear, 1=Hashed
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public int SiteId { get; set; }
    public Guid SiteGuid { get; set; }
    public string? LoginName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? AuthorBio { get; set; }
    public string? Signature { get; set; }
    
    public ICollection<LegacyUserRole> UserRoles { get; set; } = [];
}