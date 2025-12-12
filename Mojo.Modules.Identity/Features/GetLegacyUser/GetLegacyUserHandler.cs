using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Identity.Data;

namespace Mojo.Modules.Identity.Features.GetLegacyUser;

public static class GetLegacyUserHandler
{
    public static async Task<GetLegacyUserResponse> Handle(GetLegacyUserQuery query, IdentityDbContext db, CancellationToken ct)
    {
        var allWithGivenEmail = await db.LegacyUsers.AsNoTracking()
            .Include(u => u.UserRoles)
                .ThenInclude(r => r.Role)
            .Where(x => x.Email == query.Email && x.IsEmailConfirmed && !x.IsDeleted)
            .ToListAsync(ct);
        var response = new GetLegacyUserResponse
        {
            LegacyUser = allWithGivenEmail.FirstOrDefault(x => x.SiteGuid == query.SiteGuid),
            LegacyUsers = allWithGivenEmail
        };

        return response;
    }
}