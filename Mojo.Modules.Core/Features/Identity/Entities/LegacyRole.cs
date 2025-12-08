using Mojo.Modules.Core.Features.SiteStructure.Entities;

namespace Mojo.Modules.Core.Features.Identity.Entities;

public class LegacyRole
{
    public int RoleId { get; set; }

    public int SiteId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? DisplayName { get; set; }

    public Guid? SiteGuid { get; set; }

    public Guid? RoleGuid { get; set; }

    public string? Description { get; set; }

    public virtual Site Site { get; set; } = null!;
}