using Microsoft.AspNetCore.Http;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Core.Features.Identity.LoginUser;

public class LoginUserEndpoint
{
    [WolverinePost("/api/auth/login")]
    public static async Task<IResult> Post(
        LoginUserQuery query,
        IMessageBus bus)
    {
        return await bus.InvokeAsync<IResult>(query);
    }
}