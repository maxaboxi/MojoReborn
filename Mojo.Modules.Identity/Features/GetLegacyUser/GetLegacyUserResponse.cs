using Mojo.Modules.Identity.Domain.Entities;

namespace Mojo.Modules.Identity.Features.GetLegacyUser;

public class GetLegacyUserResponse
{
    public LegacyUser? LegacyUser { get; set; }
    public List<LegacyUser> LegacyUsers { get; set; } = [];
}