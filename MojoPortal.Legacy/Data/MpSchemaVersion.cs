using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpSchemaVersion
{
    public Guid ApplicationId { get; set; }

    public string ApplicationName { get; set; } = null!;

    public int Major { get; set; }

    public int Minor { get; set; }

    public int Build { get; set; }

    public int Revision { get; set; }

    public virtual ICollection<MpSchemaScriptHistory> MpSchemaScriptHistories { get; set; } = new List<MpSchemaScriptHistory>();
}
