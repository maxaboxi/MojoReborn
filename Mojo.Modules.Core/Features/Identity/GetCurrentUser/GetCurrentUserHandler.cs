using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Mojo.Modules.Core.Features.Identity.Entities;

namespace Mojo.Modules.Core.Features.Identity.GetCurrentUser;

public class GetCurrentUserHandler
{
    public async Task<GetCurrentUserResponse> Handle(
        GetCurrentUserQuery query,
        UserManager<ApplicationUser> userManager,
        ClaimsPrincipal principal)
    {
        var user = await userManager.GetUserAsync(principal);

        if (user == null)
        {
            return new GetCurrentUserResponse();
        }

        return new GetCurrentUserResponse
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
    }
}