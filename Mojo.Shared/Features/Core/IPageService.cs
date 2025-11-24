namespace Mojo.Shared.Features.Core;

public interface IPageService
{
    Task<List<PageDto>> GetMenuStructureAsync();
}