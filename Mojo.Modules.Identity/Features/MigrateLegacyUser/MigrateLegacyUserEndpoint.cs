using Microsoft.AspNetCore.Http;
using Mojo.Modules.Identity.Features.MigrateLegacyUser;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Core.Features.Identity.MigrateLegacyUser;

public class MigrateLegacyUserEndpoint
{
    [WolverinePost("/api/auth/migrate-legacy")]
    public static async Task<IResult> Post(
        MigrateLegacyUserCommand command,
        IMessageBus bus)
    {
        return await bus.InvokeAsync<IResult>(command);
    }
}