using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSurvey
{
    public Guid SurveyGuid { get; set; }

    public Guid SiteGuid { get; set; }

    public string SurveyName { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public string? StartPageText { get; set; }

    public string? EndPageText { get; set; }

    public int SubmissionLimit { get; set; }
}
