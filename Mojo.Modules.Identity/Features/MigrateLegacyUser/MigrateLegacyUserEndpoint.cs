using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Identity.Features.MigrateLegacyUser;

public class MigrateLegacyUserEndpoint
{
    [WolverinePost("/api/auth/migrate-legacy")]
    public static async Task<IResult> Post(
        [FromForm] MigrateLegacyUserCommand command,
        IMessageBus bus)
    {
        return await bus.InvokeAsync<IResult>(command);
    }
}