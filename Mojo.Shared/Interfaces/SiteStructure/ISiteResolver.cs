using Mojo.Shared.Dtos.SiteStructure;

namespace Mojo.Shared.Interfaces.SiteStructure;

public interface ISiteResolver
{
    public Task<SiteDto> GetSite(CancellationToken ct);
}