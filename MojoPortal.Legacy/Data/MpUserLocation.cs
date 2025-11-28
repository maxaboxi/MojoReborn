using System;
using System.Collections.Generic;

namespace MojoPortal.Legacy.Data;

public partial class MpUserLocation
{
    public Guid RowId { get; set; }

    public Guid UserGuid { get; set; }

    public Guid SiteGuid { get; set; }

    public string Ipaddress { get; set; } = null!;

    public long IpaddressLong { get; set; }

    public string? Hostname { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public string? Isp { get; set; }

    public string? Continent { get; set; }

    public string? Country { get; set; }

    public string? Region { get; set; }

    public string? City { get; set; }

    public string? TimeZone { get; set; }

    public int CaptureCount { get; set; }

    public DateTime FirstCaptureUtc { get; set; }

    public DateTime LastCaptureUtc { get; set; }
}
