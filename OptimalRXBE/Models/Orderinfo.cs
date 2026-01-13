using System;
using System.Collections.Generic;

namespace OptimalRXBE.Models;

public partial class Orderinfo
{
    public decimal Id { get; set; }

    public string GOrderno { get; set; } = null!;

    public string? CurrentStage { get; set; }

    public int? Stagenumber { get; set; }

    public string? ActualLensName { get; set; }

    public string? DeliveryStatusSend { get; set; }

    public string? Processedma { get; set; }

    public string? LtrueCurve { get; set; }

    public string? RtrueCurve { get; set; }

    public string? StockTransferNo { get; set; }

    public string? Readytosend { get; set; }

    public string? TrayNo { get; set; }

    public string? TrayColor { get; set; }

    public decimal? OrderDurationHour { get; set; }

    public DateTime? StockTransferDate { get; set; }

    public string? Glpl { get; set; }

    public string? Processedby { get; set; }

    public string? AwbNo { get; set; }

    public string? SendUpdateStage { get; set; }

    public string? FoundLtc { get; set; }

    public string? FoundRtc { get; set; }

    public string? Rpd { get; set; }

    public string? Lpd { get; set; }

    public string? FittingHeight { get; set; }

    public string? PentaFit { get; set; }

    public string? WrapAngle { get; set; }

    public string? Bvd { get; set; }
}
