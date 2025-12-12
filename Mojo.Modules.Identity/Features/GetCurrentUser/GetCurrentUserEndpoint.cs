using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Identity.Domain.Entities;
using Wolverine.Http;

namespace Mojo.Modules.Identity.Features.GetCurrentUser;

public class GetCurrentUserEndpoint
{
    [WolverineGet("/api/auth/user")]
    public async Task<IResult> Get(
        ClaimsPrincipal principal,
        UserManager<ApplicationUser> userManager,
        CancellationToken ct)
    {
        var userId = userManager.GetUserId(principal);
        
        if (userId == null)
        {
             return Results.Unauthorized();;
        }
        
        var user = await userManager.Users.AsNoTracking()
            .Include(u => u.UserSiteProfiles)
            .Include(u => u.UserSiteRoleAssignments)
                .ThenInclude(us => us.Role)
            .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId), ct);
        
        if (user == null)
        {
            return Results.Unauthorized();;
        }
        
        var response =  new GetCurrentUserResponse
        {
            Id = user.Id,
            Email = user.Email ?? "",
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = user.DisplayName,
            AvatarUrl = user.AvatarUrl,
            Bio =  user.Bio,
            Signature =  user.Signature,
            Roles = user.UserSiteRoleAssignments.Select(x => x.Role.Name.ToLower()).ToList()
        };
        
        return Results.Ok(response);
    }
}