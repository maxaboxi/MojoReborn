using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mojo.Modules.Identity.Data;
using Mojo.Modules.Identity.Domain.Entities;
using Mojo.Modules.Identity.Features.GetLegacyUser;
using Mojo.Shared.Interfaces.SiteStructure;
using Wolverine;

namespace Mojo.Modules.Identity.Features.LoginUser;

public class LoginUserHandler
{
    public async Task<IResult> Handle(
        LoginUserQuery query,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IMessageBus bus,
        IConfiguration configuration,
        ISiteResolver siteResolver,
        IdentityDbContext db,
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
        
        var site = await siteResolver.GetSite(ct);
        
        var legacyUserResponse = await bus.InvokeAsync<GetLegacyUserResponse>(new GetLegacyUserQuery(email, site.SiteGuid), ct);

        if (!string.IsNullOrEmpty(legacyUserResponse.LegacyUser?.Email))
        {
            return Results.Redirect($"{baseUrl}/auth/migrate-legacy");
        }
        
        var newUser = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            DisplayName = info.Principal.FindFirstValue(ClaimTypes.Name) ?? string.Empty,
            FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
            LastName = info.Principal.FindFirstValue(ClaimTypes.Surname) ?? string.Empty
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
        
        var role = await db.SiteRoles.FirstAsync(x => x.SiteGuid == site.SiteGuid && x.Name == "Authenticated Users", ct);

        var userSiteProfiles = new List<UserSiteProfile>
        {
            new()
            {
                SiteGuid = site.SiteGuid,
                SiteId = site.SiteId,
                UserId = newUser.Id,
            }
        };
        
        var userSiteRoleAssignments = new List<UserSiteRoleAssignment>
        {
            new()
            {
                UserId = newUser.Id,
                RoleId = role.Id
            }
        };
        
        var legacyUser = new LegacyUser
        {
            Email = email,
            Name = email,
            CreatedAt = DateTime.UtcNow,
            UserGuid = newUser.Id,
            SiteGuid = site.SiteGuid,
            SiteId = site.SiteId
        };
        
        await db.LegacyUsers.AddAsync(legacyUser, ct);
        await db.SaveChangesAsync(ct);
        
        newUser.UserSiteProfiles = userSiteProfiles;
        newUser.UserSiteRoleAssignments = userSiteRoleAssignments;
        newUser.LegacyId = legacyUser.UserId;
        
        var updateResult = await userManager.UpdateAsync(newUser);
        
        if (!updateResult.Succeeded)
        {
            var errors = string.Join(",", updateResult.Errors.Select(e => e.Description));
            return Results.Redirect($"{baseUrl}/auth/login?error=legacy_creation_failed&details={errors}");
        }

        await signInManager.SignInAsync(newUser, isPersistent: false);
        
        return Results.Redirect($"{baseUrl}");
    }
}