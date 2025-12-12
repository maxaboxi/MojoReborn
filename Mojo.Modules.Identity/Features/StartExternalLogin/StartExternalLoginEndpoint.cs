using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Mojo.Modules.Identity.Domain.Entities;
using Wolverine.Http;

namespace Mojo.Modules.Identity.Features.StartExternalLogin;

public class StartExternalLoginEndpoint
{
    [WolverineGet("/api/auth/external-login/{provider}")]
    public static IResult Get(string provider, SignInManager<ApplicationUser> signInManager)
    {
        var redirectUrl = "/api/auth/login";
        var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Results.Challenge(properties, [provider]);
    }
}