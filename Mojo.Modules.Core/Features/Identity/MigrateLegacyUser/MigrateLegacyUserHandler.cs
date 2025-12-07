using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Mojo.Modules.Core.Features.Identity.Entities;
using Mojo.Modules.Core.Features.Identity.GetLegacyUser;
using Wolverine;

namespace Mojo.Modules.Core.Features.Identity.MigrateLegacyUser;

public class MigrateLegacyUserHandler
{
    public async Task<IResult> Handle(
        MigrateLegacyUserCommand command,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IMessageBus bus,
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
            return Results.Redirect($"{baseUrl}/auth/login?error=email_not_found");
        }
        
        var legacyUser = await bus.InvokeAsync<LegacyUser>(new GetLegacyUserQuery(email), ct);
        
        if (string.IsNullOrEmpty(legacyUser.Email))
        {
            return Results.Redirect($"{baseUrl}/auth/login?error=email_not_found");
        }

        if (legacyUser.PwdFormat == 2)
        {
            // Would require implementing a function to mimic the old FormsAuthentication.Decrypt method
            // or find a package to do that.
            return Results.Redirect($"{baseUrl}/auth/migrate-legacy?error=account_too_old");
        }

        var passwordIsMatch = VerifyPassword(configuration, command.OldPassword, legacyUser);

        if (!passwordIsMatch)
        {
            return Results.Redirect($"{baseUrl}/auth/migrate-legacy?error=password_mismatch");
        }

        var newUser = new ApplicationUser
        {
            Id = legacyUser.UserGuid.ToString(),
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            SiteId = legacyUser.SiteId,
            SiteGuid = legacyUser.SiteGuid
        };
        
        var createResult = await userManager.CreateAsync(newUser);
        if (!createResult.Succeeded)
        {
            var errors = string.Join(",", createResult.Errors.Select(e => e.Description));
            return Results.Redirect($"{baseUrl}/auth/migrate-legacy?error=creation_failed&details={errors}");
        }

        var linkResult = await userManager.AddLoginAsync(newUser, info);
        if (!linkResult.Succeeded)
        {
            return Results.Redirect($"{baseUrl}/auth/migrate-legacy?error=linking_failed");
        }

        await signInManager.SignInAsync(newUser, isPersistent: false);
        
        return Results.Redirect($"{baseUrl}");
    }

    private static bool VerifyPassword(IConfiguration configuration, string providedPassword, LegacyUser legacyUser)
    {
        if (legacyUser.PwdFormat == 0)
        {
            return providedPassword == legacyUser.Pwd;
        }
        
        if (string.IsNullOrEmpty(legacyUser.PasswordHash) || string.IsNullOrEmpty(legacyUser.PasswordSalt))
        {
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

}