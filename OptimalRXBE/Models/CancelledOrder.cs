using System;
using System.Collections.Generic;

namespace OptimalRXBE.Models;

public partial class CancelledOrder
{
    public int Id { get; set; }

    public string GOrderNo { get; set; } = null!;

    public string OrderNo { get; set; } = null!;

    public string? PartyCode { get; set; }

    public string? Remarks { get; set; }

    public string? Reason { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UserName { get; set; }

    public string? CreditNoteNo { get; set; }

    public string? Category { get; set; }

    public int? Lqty { get; set; }

    public int? Rqty { get; set; }
}
