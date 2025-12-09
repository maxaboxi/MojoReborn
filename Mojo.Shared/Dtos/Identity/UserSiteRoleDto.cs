namespace Mojo.Shared.Dtos.Identity;

public record UserSiteRoleDto(Guid Id, int SiteId, Guid? SiteGuid, string Name, string DisplayName, string? Description);