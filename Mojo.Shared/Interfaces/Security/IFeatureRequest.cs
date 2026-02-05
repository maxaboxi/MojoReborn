namespace Mojo.Shared.Interfaces.Security;

/// <summary>
/// Marker interface for commands that require feature-level security validation.
/// Messages implementing this interface are automatically intercepted by
/// <c>FeatureSecurityMiddleware</c>, which resolves the module context and
/// verifies the user has appropriate permissions before the handler executes.
/// </summary>
public interface IFeatureRequest
{
    int PageId { get; }
    string Name { get; }
    bool RequiresEditPermission { get; }
    bool UserCanEdit { get; }
}
