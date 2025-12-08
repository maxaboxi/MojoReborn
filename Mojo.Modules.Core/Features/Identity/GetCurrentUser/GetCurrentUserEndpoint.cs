using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Mojo.Modules.Core.Features.Identity.Entities;
using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Core.Features.Identity.GetCurrentUser;

public class GetCurrentUserEndpoint
{
    [WolverineGet("/api/auth/user")]
    public async Task<IResult> Get(
        GetCurrentUserQuery query,
        IMessageBus bus)
    {
        var response = await bus.InvokeAsync<GetCurrentUserResponse>(query);

        return response.Id == Guid.Empty ? Results.Unauthorized() : Results.Ok(response);
    }
}