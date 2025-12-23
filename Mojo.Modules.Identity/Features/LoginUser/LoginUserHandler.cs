using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        ILogger<LoginUserHandler> logger,
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
            logger.LogError("Error loading external login information.");
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
            logger.LogError("Email missing from external login information.");
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
            logger.LogError("Creating the user failed: {errors}", errors);
            return Results.Redirect($"{baseUrl}/auth/login?error=creation_failed&details={errors}");
        }

        var linkResult = await userManager.AddLoginAsync(newUser, info);
        
        if (!linkResult.Succeeded)
        {
            logger.LogError("Linking external login info to user failed: {@Errors}", linkResult.Errors.Select(e => e.Description));
            return Results.Redirect($"{baseUrl}/auth/login?error=linking_failed");
        }
        
        var roleName = configuration["Authentication:AuthenticatedUsersRoleName"];

        if (roleName == null)
        {
            throw new Exception("AuthenticatedUsersRoleName is not configured");
        }
        
        var role = await db.SiteRoles.FirstOrDefaultAsync(x => x.SiteGuid == site.SiteGuid && x.Name == roleName, ct);

        if (role == null)
        {
            logger.LogError("AuthenticatedUsersRoleName {Role} missing from database for Site: Id {Id}, Guid {Guid}", roleName, site.SiteId, site.SiteGuid);
            return Results.Redirect($"{baseUrl}/auth/login?error=legacy_creation_failed_role_missing");
        }

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
            logger.LogError("Updating the user account failed: {errors}", errors);
            return Results.Redirect($"{baseUrl}/auth/login?error=legacy_creation_failed&details={errors}");
        }

        await signInManager.SignInAsync(newUser, isPersistent: false);
        
        return Results.Redirect($"{baseUrl}");
    }
}