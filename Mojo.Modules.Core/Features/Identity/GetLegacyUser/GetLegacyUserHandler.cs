using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Core.Data;
using Mojo.Modules.Core.Features.Identity.Entities;

namespace Mojo.Modules.Core.Features.Identity.GetLegacyUser;

public static class GetLegacyUserHandler
{
    public static async Task<LegacyUser> Handle(GetLegacyUserQuery query, CoreDbContext db, CancellationToken ct)
    {
        return await db.LegacyUsers.AsNoTracking().Where(x => x.Email == query.Email).FirstOrDefaultAsync(ct) ?? new LegacyUser();
    }
}