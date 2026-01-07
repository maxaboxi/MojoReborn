namespace Mojo.Shared.Interfaces.Security;

public interface IFeatureRequest
{
    int PageId { get; }
    string Name { get; }
    bool RequiresEditPermission { get; }
    bool UserCanEdit { get; }
}
