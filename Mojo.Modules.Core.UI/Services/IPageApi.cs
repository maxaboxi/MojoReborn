using Mojo.Shared.Features.Core;
using Refit;

namespace Mojo.Modules.Core.UI.Services;

public interface IPageApi
{
    [Get("/api/core/menu")]
    Task<List<PageDto>> GetPagesAsync();
}