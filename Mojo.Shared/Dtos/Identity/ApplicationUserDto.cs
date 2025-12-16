namespace Mojo.Shared.Dtos.Identity;

public record ApplicationUserDto(
    Guid Id,
    string Email, 
    string FirstName, 
    string LastName, 
    string DisplayName, 
    string? Bio, 
    string? Signature, 
    int? LegacyId,
    string? AvatarUrl, 
    string? TimeZoneId,
    List<UserSiteProfileDto> UserSiteProfiles, 
    List<UserSiteRoleDto> UserSiteRoles);