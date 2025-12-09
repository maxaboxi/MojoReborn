using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Mojo.Modules.Core.Features.Identity.Entities;
using Wolverine.Http;

namespace Mojo.Modules.Core.Features.Identity.GetCurrentUser;

public class GetCurrentUserEndpoint
{
    [WolverineGet("/api/auth/user")]
    public async Task<IResult> Get(
        ClaimsPrincipal principal,
        UserManager<ApplicationUser> userManager)
    {
        var user = await userManager.GetUserAsync(principal);

        if (user == null)
        {
            return Results.Unauthorized();
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
            Signature =  user.Signature
        };
        
        return Results.Ok(response);
    }
}