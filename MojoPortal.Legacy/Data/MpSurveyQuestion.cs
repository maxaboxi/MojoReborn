using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSurveyQuestion
{
    public Guid QuestionGuid { get; set; }

    public Guid PageGuid { get; set; }

    public string? QuestionText { get; set; }

    public int QuestionTypeId { get; set; }

    public bool AnswerIsRequired { get; set; }

    public int QuestionOrder { get; set; }

    public string? ValidationMessage { get; set; }

    public string? QuestionName { get; set; }
}
