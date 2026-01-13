using System;
using System.Collections.Generic;

namespace OptimalRXBE.Models;

public partial class Accessibility
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? Passwd { get; set; }

    public string? Master { get; set; }

    public string? Stock { get; set; }

    public string? Measurements { get; set; }

    public string? Orders { get; set; }

    public string? BatchPro { get; set; }

    public string? Invoice { get; set; }

    public string? Reports { get; set; }

    public string? View { get; set; }

    public string? Breakage { get; set; }

    public string? MonthEndRep { get; set; }

    public string? Payment { get; set; }

    public string? Lens { get; set; }

    public string? UserType { get; set; }

    public string? FindorderstatusReport { get; set; }

    public int? Branchid { get; set; }

    public bool? IsServerRunning { get; set; }
}
