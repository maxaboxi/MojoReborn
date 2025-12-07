using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Mojo.Modules.Core.Features.Identity.Entities;
using Mojo.Modules.Core.Features.Identity.GetLegacyUser;
using Mojo.Modules.Core.Features.SiteStructure.GetSite;
using Wolverine;

namespace Mojo.Modules.Core.Features.Identity.LoginUser;

public class LoginUserHandler
{
    public async Task<IResult> Handle(
        LoginUserQuery query, 
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IMessageBus bus,
        IConfiguration configuration,
        SiteResolver siteResolver,
        CancellationToken ct)
    {
        var baseUrl = configuration["Frontend:Url"];

        if (baseUrl == null)
        {
            throw new Exception("Frontend Url is not configured");
        }
        
        var info = await signInManager.GetExternalLoginInfoAsync();
        
        if (info == null)
        {
            return Results.Redirect($"{baseUrl}/auth/login?error=session_expired");
        }

        var result = await signInManager.ExternalLoginSignInAsync(
            info.LoginProvider, 
            info.ProviderKey, 
            isPersistent: false);

        if (result.Succeeded)
        {
            return Results.Redirect($"{baseUrl}");
        }
        
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(email))
        {
            return Results.Redirect($"{baseUrl}/auth/login?error=email_not_found");
        }
        
        var legacyUser = await bus.InvokeAsync<LegacyUser>(new GetLegacyUserQuery(email), ct);

        if (!string.IsNullOrEmpty(legacyUser.Email))
        {
            return Results.Redirect($"{baseUrl}/auth/migrate-legacy");
        }

        var site = await siteResolver.GetSite(ct);

        var newUser = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            SiteId = site.SiteId,
            SiteGuid = site.SiteGuid
        };

        var createResult = await userManager.CreateAsync(newUser);
        if (!createResult.Succeeded)
        {
            var errors = string.Join(",", createResult.Errors.Select(e => e.Description));
            return Results.Redirect($"{baseUrl}/auth/login?error=creation_failed&details={errors}");
        }

        var linkResult = await userManager.AddLoginAsync(newUser, info);
        if (!linkResult.Succeeded)
        {
            return Results.Redirect($"{baseUrl}/auth/login?error=linking_failed");
        }

        await signInManager.SignInAsync(newUser, isPersistent: false);
        
        return Results.Redirect($"{baseUrl}");
    }
}