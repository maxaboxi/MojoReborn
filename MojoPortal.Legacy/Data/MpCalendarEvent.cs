using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpCalendarEvent
{
    public int ItemId { get; set; }

    public int ModuleId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? ImageName { get; set; }

    public DateTime? EventDate { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UserId { get; set; }

    public Guid? ItemGuid { get; set; }

    public Guid? ModuleGuid { get; set; }

    public Guid? UserGuid { get; set; }

    public string? Location { get; set; }

    public Guid? LastModUserGuid { get; set; }

    public DateTime? LastModUtc { get; set; }

    public decimal? TicketPrice { get; set; }

    public bool? RequiresTicket { get; set; }

    public bool ShowMap { get; set; }
}
