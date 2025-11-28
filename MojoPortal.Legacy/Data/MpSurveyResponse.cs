using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSurveyResponse
{
    public Guid ResponseGuid { get; set; }

    public Guid SurveyGuid { get; set; }

    public DateTime? SubmissionDate { get; set; }

    public bool Annonymous { get; set; }

    public bool Complete { get; set; }

    public Guid? UserGuid { get; set; }
}
