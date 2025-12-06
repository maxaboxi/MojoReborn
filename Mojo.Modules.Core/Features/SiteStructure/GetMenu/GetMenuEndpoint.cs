using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.Core.Features.SiteStructure.GetMenu;

public class GetMenuEndpoint
{
    [WolverineGet("/api/core/menu")]
    public static Task<List<PageDto>> Get(GetMenuQuery query, IMessageBus bus)
    {
        return bus.InvokeAsync<List<PageDto>>(query);
    }
}