using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class I7SflexiField
{
    public Guid SiteGuid { get; set; }

    public Guid FeatureGuid { get; set; }

    public Guid DefinitionGuid { get; set; }

    public Guid FieldGuid { get; set; }

    public string? DefinitionName { get; set; }

    public string? Name { get; set; }

    public string? Label { get; set; }

    public string? DefaultValue { get; set; }

    public string? ControlType { get; set; }

    public string? ControlSrc { get; set; }

    public int? SortOrder { get; set; }

    public string? HelpKey { get; set; }

    public bool? Required { get; set; }

    public string? RequiredMessageFormat { get; set; }

    public string? Regex { get; set; }

    public string RegexMessageFormat { get; set; } = null!;

    public string? Token { get; set; }

    public bool? Searchable { get; set; }

    public string? EditPageControlWrapperCssClass { get; set; }

    public string? EditPageLabelCssClass { get; set; }

    public string? EditPageControlCssClass { get; set; }

    public bool? DatePickerIncludeTimeForDate { get; set; }

    public bool? DatePickerShowMonthList { get; set; }

    public bool? DatePickerShowYearList { get; set; }

    public string? DatePickerYearRange { get; set; }

    public string? ImageBrowserEmptyUrl { get; set; }

    public string? Options { get; set; }

    public bool? CheckBoxReturnBool { get; set; }

    public string? CheckBoxReturnValueWhenTrue { get; set; }

    public string? CheckBoxReturnValueWhenFalse { get; set; }

    public string? DateFormat { get; set; }

    public string? TextBoxMode { get; set; }

    public string? Attributes { get; set; }

    public bool? IsDeleted { get; set; }

    public string? PreTokenString { get; set; }

    public string? PostTokenString { get; set; }

    public bool? IsGlobal { get; set; }

    public string ViewRoles { get; set; } = null!;

    public string EditRoles { get; set; } = null!;

    public string PreTokenStringWhenTrue { get; set; } = null!;

    public string PostTokenStringWhenTrue { get; set; } = null!;

    public string PreTokenStringWhenFalse { get; set; } = null!;

    public string PostTokenStringWhenFalse { get; set; } = null!;

    public string? DataType { get; set; }

    public bool? IsList { get; set; }
}
