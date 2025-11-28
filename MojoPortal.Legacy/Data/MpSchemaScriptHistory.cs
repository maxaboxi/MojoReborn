using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSchemaScriptHistory
{
    public int Id { get; set; }

    public Guid ApplicationId { get; set; }

    public string ScriptFile { get; set; } = null!;

    public DateTime RunTime { get; set; }

    public bool ErrorOccurred { get; set; }

    public string? ErrorMessage { get; set; }

    public string? ScriptBody { get; set; }

    public virtual MpSchemaVersion Application { get; set; } = null!;
}
