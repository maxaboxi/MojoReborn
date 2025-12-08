using Mojo.Modules.Core.Features.Identity.Entities;

namespace Mojo.Modules.Core.Features.Identity.GetLegacyUser;

public class GetLegacyUserResponse
{
    public LegacyUser? LegacyUser { get; set; }
    public List<LegacyUser> LegacyUsers { get; set; } = [];
}