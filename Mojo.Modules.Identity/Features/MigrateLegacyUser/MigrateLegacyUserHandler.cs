using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Mojo.Modules.Identity.Data;
using Mojo.Modules.Identity.Domain.Entities;
using Mojo.Modules.Identity.Features.GetLegacyUser;
using Mojo.Shared.Interfaces.SiteStructure;
using Wolverine;

namespace Mojo.Modules.Identity.Features.MigrateLegacyUser;

public class MigrateLegacyUserHandler
{
    public async Task<IResult> Handle(
        MigrateLegacyUserCommand command,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        ISiteResolver siteResolver,
        IConfiguration configuration,
        IMessageBus bus,
        IdentityDbContext db,
        ILogger<MigrateLegacyUserHandler> logger,
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
        
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(email))
        {
            logger.LogInformation("Email missing from external login info");
            return Results.Redirect($"{baseUrl}/auth/login?error=email_not_found");
        }
        
        var site = await siteResolver.GetSite(ct);
        
        var legacyUserResponse = await bus.InvokeAsync<GetLegacyUserResponse>(new GetLegacyUserQuery(email, site.SiteGuid), ct);
        
        if (string.IsNullOrEmpty(legacyUserResponse.LegacyUser?.Email))
        {
            logger.LogInformation("User not found");
            return Results.Redirect($"{baseUrl}/auth/login?error=email_not_found");
        }
        
        var legacyUser = legacyUserResponse.LegacyUser;

        if (legacyUser.PwdFormat == 2)
        {
            // Would require implementing a function to mimic the old FormsAuthentication.Decrypt method
            // or find a package to do that.
            logger.LogInformation("Password format 2, account too old.");
            return Results.Redirect($"{baseUrl}/auth/migrate-legacy?error=account_too_old");
        }

        var passwordIsMatch = VerifyPassword(configuration, command.OldPassword, legacyUser, logger);

        if (!passwordIsMatch)
        {
            logger.LogInformation("Password doesn't match");
            return Results.Redirect($"{baseUrl}/auth/migrate-legacy?error=password_mismatch");
        }

        var newUser = new ApplicationUser
        {
            Id = legacyUser.UserGuid,
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            DisplayName = legacyUser.LoginName ?? string.Empty,
            FirstName = legacyUser.FirstName ?? string.Empty,
            LastName = legacyUser.LastName ?? string.Empty,
            Signature = legacyUser.Signature,
            LegacyId = legacyUser.UserId,
            Bio = legacyUser.AuthorBio
        };
        
        var createResult = await userManager.CreateAsync(newUser);
        if (!createResult.Succeeded)
        {
            var errors = string.Join(",", createResult.Errors.Select(e => e.Description));
            logger.LogError("Creating the user failed: {errors}", errors);
            return Results.Redirect($"{baseUrl}/auth/migrate-legacy?error=creation_failed&details={errors}");
        }

        var linkResult = await userManager.AddLoginAsync(newUser, info);
        if (!linkResult.Succeeded)
        {
            logger.LogError("Linking external login info to user failed: {@Errors}", linkResult.Errors.Select(e => e.Description));
            return Results.Redirect($"{baseUrl}/auth/migrate-legacy?error=linking_failed");
        }
        
        var isSuccess = await MapUserRoles(legacyUserResponse, newUser, db, logger, ct);

        if (!isSuccess)
        {
            return Results.Redirect($"{baseUrl}/auth/migrate-legacy?error=role_migration_failed");
        }

        await signInManager.SignInAsync(newUser, isPersistent: false);
        
        return Results.Redirect($"{baseUrl}");
    }

    private static bool VerifyPassword(IConfiguration configuration, string providedPassword, LegacyUser legacyUser, ILogger<MigrateLegacyUserHandler> logger)
    {
        if (legacyUser.PwdFormat == 0)
        {
            return providedPassword == legacyUser.Pwd;
        }
        
        if (string.IsNullOrEmpty(legacyUser.PasswordHash) || string.IsNullOrEmpty(legacyUser.PasswordSalt))
        {
            logger.LogError("Password hash {passwordHash} or password salt {passwordSalt} is missing.", legacyUser.PasswordHash, legacyUser.PasswordSalt);
            return false;
        }
        
        var algorithm = configuration["LegacyAuth:HashAlgorithm"];
        
        if (algorithm == null)
        {
            throw new Exception("Hash algorithm is not configured");
        }
        
        var saltBytes = Convert.FromBase64String(legacyUser.PasswordSalt);
        var passwordBytes = Encoding.Unicode.GetBytes(providedPassword);
        var combinedBytes = new byte[saltBytes.Length + passwordBytes.Length];
        
        Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, saltBytes.Length);
        Buffer.BlockCopy(passwordBytes, 0, combinedBytes, saltBytes.Length, passwordBytes.Length);

        using var hashAlgorithm = CreateHashAlgorithm(algorithm, configuration);
        var hashBytes = hashAlgorithm.ComputeHash(combinedBytes);
        var computedHash = Convert.ToBase64String(hashBytes);
            
        return computedHash == legacyUser.PasswordHash;
    }

    private static HashAlgorithm CreateHashAlgorithm(string algorithm, IConfiguration configuration)
    {
        algorithm = algorithm.ToUpperInvariant();

        if (!algorithm.StartsWith("HMAC"))
            return algorithm switch
            {
                "SHA512" => SHA512.Create(),
                "SHA1" => SHA1.Create(),
                "SHA256" => SHA256.Create(),
                "MD5" => MD5.Create(),
                _ => throw new NotSupportedException($"Legacy Algorithm '{algorithm}' is not supported.")
            };
        
        var validationKey = configuration["LegacyAuth:MachineKey:ValidationKey"];

        if (string.IsNullOrEmpty(validationKey))
        {
            throw new Exception("Validation key is not configured");
        }
        
        var key = Convert.FromHexString(validationKey);

        return algorithm switch
        {
            "HMACMD5" => new HMACMD5(key),
            "HMACSHA1" => new HMACSHA1(key),
            "HMACSHA256" => new HMACSHA256(key),
            "HMACSHA512" => new HMACSHA512(key),
            _ => throw new NotSupportedException($"Legacy Algorithm '{algorithm}' is not supported.")
        };
    }

    /// <summary>
    /// mojoPortal can have same user (email) multiple times in mp_Users table (one for every site)
    /// and that is not something this application is going to support.
    /// Instead, AspNetUsers has RequireUniqueEmail set to true.
    /// New database tables are added for storing the information about the users role(s) for site(s).
    /// </summary>
    /// <param name="response">Contains the users found with the same email that are not deleted and who's email is confirmed.</param>
    /// <param name="newUser">The new ApplicationUser entity</param>
    /// <param name="db">IdentityDbContext</param>
    /// <param name="logger"></param>
    /// <param name="ct"></param>
    /// <returns>Boolean to tell whether the role migration was done successfully</returns>
    private static async Task<bool> MapUserRoles(
        GetLegacyUserResponse response, 
        ApplicationUser newUser, 
        IdentityDbContext db, 
        ILogger<MigrateLegacyUserHandler> logger,
        CancellationToken ct)
    {
        var siteProfiles = new List<UserSiteProfile>();
        var siteRoleAssignments = new List<UserSiteRoleAssignment>();

        foreach (var user in response.LegacyUsers)
        {
            if (siteProfiles.Any(p => p.SiteGuid == user.SiteGuid))
            {
                continue;
            }

            var siteProfile = new UserSiteProfile
            {
                UserId = newUser.Id,
                SiteGuid = user.SiteGuid,
                SiteId = user.SiteId,
            };

            siteRoleAssignments.AddRange(user.UserRoles.Select(role => new UserSiteRoleAssignment { UserId = newUser.Id, RoleId = role.RoleGuid }));
            siteProfiles.Add(siteProfile);
        }

        try
        {
            db.UserSiteProfiles.AddRange(siteProfiles);
            db.UserSiteRoleAssignments.AddRange(siteRoleAssignments);
            await db.SaveChangesAsync(ct);
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while trying to map legacy user profiles");
            return false;
        }
    }

}