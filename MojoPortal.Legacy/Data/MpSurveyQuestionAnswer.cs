using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSurveyQuestionAnswer
{
    public Guid AnswerGuid { get; set; }

    public Guid QuestionGuid { get; set; }

    public Guid ResponseGuid { get; set; }

    public string? Answer { get; set; }

    public DateTime AnsweredDate { get; set; }
}
