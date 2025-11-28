using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSurveyQuestionOption
{
    public Guid QuestionOptionGuid { get; set; }

    public Guid QuestionGuid { get; set; }

    public string Answer { get; set; } = null!;

    public int Order { get; set; }
}
