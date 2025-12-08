using Microsoft.AspNetCore.Http;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Core.Features.Identity.LoginUser;

public class LoginUserEndpoint
{
    [WolverineGet("/api/auth/login")]
    public static async Task<IResult> Get(
        LoginUserQuery query,
        IMessageBus bus)
    {
        return await bus.InvokeAsync<IResult>(query);
    }
}