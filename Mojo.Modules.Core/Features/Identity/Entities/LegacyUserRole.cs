namespace Mojo.Modules.Core.Features.Identity.Entities;

public class LegacyUserRole
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public Guid UserGuid { get; set; }
    public int RoleId { get; set; }
    public Guid RoleGuid { get; set; }
    public virtual LegacyUser User { get; set; }
    public virtual LegacyRole Role { get; set; }
}