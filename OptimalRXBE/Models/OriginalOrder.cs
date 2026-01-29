using System;
using System.Collections.Generic;

namespace OptimalRXBE.Models;

public partial class OriginalOrder
{
    public string GOrderNo { get; set; } = null!;

    public string OrderNo { get; set; } = null!;

    public string? PartyCode { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? LensFrameBoth { get; set; }

    public string? SingleVisionMultiFocal { get; set; }

    public string? LensType { get; set; }

    public string? Rsph { get; set; }

    public string? Rcyl { get; set; }

    public string? Raxis { get; set; }

    public string? Raddn { get; set; }

    public string? Rqty { get; set; }

    public double? Rrate { get; set; }

    public double? Rdiscount { get; set; }

    public string? Lsph { get; set; }

    public string? Lcyl { get; set; }

    public string? Laxis { get; set; }

    public string? Laddn { get; set; }

    public string? Lqty { get; set; }

    public double? Lrate { get; set; }

    public double? Ldiscount { get; set; }

    public string? Coating { get; set; }

    public string? TintColor { get; set; }

    public string? CoatColor { get; set; }

    public string? Fitting { get; set; }

    public string? Stock { get; set; }

    public double? Rate { get; set; }

    public string? Remarks { get; set; }

    public DateTime? OrderEntryTime { get; set; }

    public string? PartyOrderRefNo { get; set; }

    public string? RegisterNo { get; set; }

    public string? PartyCustomerName { get; set; }

    public string? DummyLensType { get; set; }

    public string? DummyRaddn { get; set; }

    public string? DummyLaddn { get; set; }

    public string? DummyRsph { get; set; }

    public string? DummyRcyl { get; set; }

    public string? DummyRaxis { get; set; }

    public string? DummyLsph { get; set; }

    public string? DummyLcyl { get; set; }

    public string? DummyLaxis { get; set; }

    public string? Senderordno { get; set; }

    public string? Sendtolabcode { get; set; }
}
