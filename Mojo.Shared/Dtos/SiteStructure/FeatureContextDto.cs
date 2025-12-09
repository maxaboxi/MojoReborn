namespace Mojo.Shared.Dtos.SiteStructure;

public record FeatureContextDto(int ModuleId, Guid ModuleGuid, Guid FeatureGuid, int SiteId, Guid SiteGuid, PageDto PageDto);