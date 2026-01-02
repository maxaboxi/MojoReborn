using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Wolverine.Http;

namespace Mojo.Modules.Identity.Features.DevLogin;

public class DevLoginEndpoint
{
    [WolverineGet("/api/auth/dev-login")]
    public static async Task<IResult> Get(
        string? email, 
        HttpContext context,
        IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            return Results.NotFound();
        }
        
        var userEmail = email ?? "dev@user.com";
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userEmail),
            new(ClaimTypes.Name, userEmail),
            new(ClaimTypes.Email, userEmail)
        };

        var identity = new ClaimsIdentity(claims, IdentityConstants.ExternalScheme);
        var principal = new ClaimsPrincipal(identity);

        var props = new AuthenticationProperties();
        props.SetString("LoginProvider", "Dev");
        props.SetString("LoginProviderKey", userEmail);

        await context.SignInAsync(IdentityConstants.ExternalScheme, principal, props);

        return Results.Redirect("/api/auth/login");
    }
}
