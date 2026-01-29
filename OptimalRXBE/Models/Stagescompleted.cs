using System;
using System.Collections.Generic;

namespace OptimalRXBE.Models;

public partial class Stagescompleted
{
    public string? LabNo { get; set; }

    public string? GOrderno { get; set; }

    public string? StageName { get; set; }

    public DateTime? CompletionDate { get; set; }

    public DateTime? CompletionTime { get; set; }

    public string? LensType { get; set; }

    public int? Qty { get; set; }

    public string? UserName { get; set; }

    public int? StageNumber { get; set; }

    public string? LohPort { get; set; }

    public decimal Id { get; set; }
}
