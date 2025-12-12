using Wolverine;
using Wolverine.Http;

namespace Mojo.Modules.SiteStructure.Features.GetMenu;

public class GetMenuEndpoint
{
    [WolverineGet("/api/site/menu")]
    public static Task<List<PageDto>> Get(GetMenuQuery query, IMessageBus bus)
    {
        return bus.InvokeAsync<List<PageDto>>(query);
    }
}