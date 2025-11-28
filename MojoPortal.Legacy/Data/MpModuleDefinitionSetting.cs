using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpModuleDefinitionSetting
{
    public int Id { get; set; }

    public int ModuleDefId { get; set; }

    public string SettingName { get; set; } = null!;

    public string? SettingValue { get; set; }

    public string? ControlType { get; set; }

    public string? RegexValidationExpression { get; set; }

    public Guid FeatureGuid { get; set; }

    public string? ResourceFile { get; set; }

    public string? ControlSrc { get; set; }

    public int SortOrder { get; set; }

    public string? HelpKey { get; set; }

    public string? GroupName { get; set; }

    public string Attributes { get; set; } = null!;

    public string Options { get; set; } = null!;
}
