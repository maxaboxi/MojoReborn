namespace Mojo.Modules.Core.Features.Identity.GetCurrentUser;

public class GetCurrentUserResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string? Bio { get; set; }
    public string? Signature { get; set; }
    
    public string? TimeZoneId { get; set; }
}