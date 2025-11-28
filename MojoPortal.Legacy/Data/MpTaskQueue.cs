using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpTaskQueue
{
    public Guid Guid { get; set; }

    public Guid SiteGuid { get; set; }

    public Guid QueuedBy { get; set; }

    public string TaskName { get; set; } = null!;

    public bool NotifyOnCompletion { get; set; }

    public string? NotificationToEmail { get; set; }

    public string? NotificationFromEmail { get; set; }

    public string? NotificationSubject { get; set; }

    public string? TaskCompleteMessage { get; set; }

    public DateTime? NotificationSentUtc { get; set; }

    public bool CanStop { get; set; }

    public bool CanResume { get; set; }

    public int UpdateFrequency { get; set; }

    public DateTime QueuedUtc { get; set; }

    public DateTime? StartUtc { get; set; }

    public DateTime? CompleteUtc { get; set; }

    public DateTime? LastStatusUpdateUtc { get; set; }

    public double CompleteRatio { get; set; }

    public string? Status { get; set; }

    public string? SerializedTaskObject { get; set; }

    public string? SerializedTaskType { get; set; }
}
