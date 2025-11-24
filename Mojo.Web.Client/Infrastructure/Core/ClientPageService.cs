using Mojo.Modules.Core.UI.Services;
using Mojo.Shared.Features.Core;

namespace Mojo.Web.Client.Infrastructure.Core;

public class ClientPageService(IPageApi api) : IPageService
{
    public async Task<List<PageDto>> GetMenuStructureAsync()
    {
        return await api.GetPagesAsync();
    }
}