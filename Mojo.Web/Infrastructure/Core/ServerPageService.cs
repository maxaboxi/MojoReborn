using Microsoft.Extensions.Caching.Memory;
using Mojo.Shared.Features.Core;
using Wolverine;

namespace Mojo.Web.Infrastructure.Core;

public class ServerPageService(IMessageBus bus, IMemoryCache cache) : IPageService
{
    public async Task<List<PageDto>> GetMenuStructureAsync()
    {
        // Cache the menu for 10 minutes. 
        // Real CMSs invalidate this cache when a page is published.
        return await cache.GetOrCreateAsync("Main_Nav_Menu", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            return await bus.InvokeAsync<List<PageDto>>(new GetMenuQuery());
        });
    }
}