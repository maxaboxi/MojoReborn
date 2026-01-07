using Mojo.Shared.Dtos.Identity;
using Mojo.Shared.Dtos.SiteStructure;

namespace Mojo.Shared.Domain;

public record SecurityContext(ApplicationUserDto User, FeatureContextDto FeatureContext, bool IsAdmin);