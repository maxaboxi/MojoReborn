using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSurveyPage
{
    public Guid PageGuid { get; set; }

    public Guid SurveyGuid { get; set; }

    public string PageTitle { get; set; } = null!;

    public int PageOrder { get; set; }

    public bool PageEnabled { get; set; }
}
