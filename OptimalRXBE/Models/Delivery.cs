using System;
using System.Collections.Generic;

namespace OptimalRXBE.Models;

public partial class Delivery
{
    public int? DCcode { get; set; }

    public string? GOrderNo { get; set; }

    public string? ModeofDel { get; set; }

    public string? ConsignmentNo { get; set; }

    public DateTime? Ddate { get; set; }

    public DateTime? Dtime { get; set; }

    public string? ChallanNo { get; set; }

    public DateTime? ChallanDate { get; set; }

    public DateTime? ConsDateTime { get; set; }

    public decimal Id { get; set; }
}
