using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpModuleSetting
{
    public int Id { get; set; }

    public int ModuleId { get; set; }

    public string SettingName { get; set; } = null!;

    public string? SettingValue { get; set; }

    public string? ControlType { get; set; }

    public string? RegexValidationExpression { get; set; }

    public Guid? SettingGuid { get; set; }

    public Guid? ModuleGuid { get; set; }

    public string? ControlSrc { get; set; }

    public int SortOrder { get; set; }

    public string? HelpKey { get; set; }

    public virtual MpModule Module { get; set; } = null!;
}
