using Mojo.Shared.Dtos.Identity;
using Mojo.Shared.Dtos.SiteStructure;

namespace Mojo.Shared.Domain;

/// <summary>
/// Encapsulates the authenticated user, resolved feature context, and admin status
/// for a secured request. Produced by <c>FeatureSecurityMiddleware</c> and injected
/// into handlers that process <see cref="Mojo.Shared.Interfaces.Security.IFeatureRequest"/> messages.
/// </summary>
public record SecurityContext(ApplicationUserDto User, FeatureContextDto FeatureContext, bool IsAdmin);