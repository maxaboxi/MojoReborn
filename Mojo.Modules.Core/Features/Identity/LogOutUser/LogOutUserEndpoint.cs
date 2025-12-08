using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Mojo.Modules.Core.Features.Identity.Entities;
using Wolverine.Http;

namespace Mojo.Modules.Core.Features.Identity.LogOutUser;

public class LogOutUserEndpoint
{
    [WolverinePost("/api/auth/logout")]
    public static async Task<IResult> Post(SignInManager<ApplicationUser> signInManager)
    {
        await signInManager.SignOutAsync();
        
        return Results.Ok();
    }
}