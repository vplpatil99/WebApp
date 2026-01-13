using System;
using System.Collections.Generic;

namespace OptimalRXBE.Models;

public partial class OrderMaster
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

    public string? Rbase { get; set; }

    public string? REt { get; set; }

    public string? RCt { get; set; }

    public string? Rtool { get; set; }

    public string? Rstock { get; set; }

    public string? Rprocessed { get; set; }

    public string? Lsph { get; set; }

    public string? Lcyl { get; set; }

    public string? Laxis { get; set; }

    public string? Laddn { get; set; }

    public string? Lqty { get; set; }

    public double? Lrate { get; set; }

    public double? Ldiscount { get; set; }

    public string? Lbase { get; set; }

    public string? LEt { get; set; }

    public string? LCt { get; set; }

    public string? Ltool { get; set; }

    public string? Lstock { get; set; }

    public string? Lprocessed { get; set; }

    public string? Coating { get; set; }

    public string? TintColor { get; set; }

    public string? CoatColor { get; set; }

    public string? Fitting { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public double? Rate { get; set; }

    public string? Status { get; set; }

    public string? Processed { get; set; }

    public string? ChallanNo { get; set; }

    public DateTime? ChallanDate { get; set; }

    public string? InvoiceNo { get; set; }

    public DateTime? InvoiceDate { get; set; }

    public string? Printed { get; set; }

    public string? Remarks { get; set; }

    public DateTime? OrderEntryTime { get; set; }

    public DateTime? OrderCompleteDateTime { get; set; }

    public string? Lprism { get; set; }

    public string? Rprism { get; set; }

    public double? LensSize { get; set; }

    public double? LfinalCt { get; set; }

    public double? RfinalCt { get; set; }

    public double? LfinalEt { get; set; }

    public double? RfinalEt { get; set; }

    public string? LblankBrand { get; set; }

    public string? LblankSupplier { get; set; }

    public string? RblankBrand { get; set; }

    public string? RblankSupplier { get; set; }

    public double? LbaseCurve { get; set; }

    public double? LbaseSag { get; set; }

    public double? Lsphsag { get; set; }

    public double? Lcylsag { get; set; }

    public double? RbaseCurve { get; set; }

    public double? RbaseSag { get; set; }

    public double? Rsphsag { get; set; }

    public double? Rcylsag { get; set; }

    public string? PartyPlace { get; set; }

    public DateTime? MustGoOrderDate { get; set; }

    public DateTime? MustGoOrderTime { get; set; }

    public string? RegisterNo { get; set; }

    public double? ActualRate { get; set; }

    public double? DiscountRate { get; set; }

    public double? AdditionalRate { get; set; }

    public DateTime? MustGoCoatDate { get; set; }

    public DateTime? MustGoCoatTime { get; set; }

    public string? UserName { get; set; }

    public string? OrdRecievdBy { get; set; }

    public string? StockOrder { get; set; }

    public string? PrecisionOrder { get; set; }

    public string? OrderD { get; set; }

    public string? ONumber { get; set; }

    public string? TrayColor { get; set; }

    public string? UrgentOrder { get; set; }

    public string? ToolRemarks { get; set; }

    public string? FinishedLensSource { get; set; }

    public string? PartyOrderRefNo { get; set; }

    public string? RfinishedStkTakenUc { get; set; }

    public string? LfinishedStkTakenUc { get; set; }

    public int? RtoolsFound { get; set; }

    public int? LtoolsFound { get; set; }

    public double? Rtc { get; set; }

    public double? Ltc { get; set; }

    public double? RAbase { get; set; }

    public double? LAbase { get; set; }

    public string? RManual { get; set; }

    public string? LManual { get; set; }

    public string? RCustomBase { get; set; }

    public string? LCustomBase { get; set; }

    public double? RCustomEt { get; set; }

    public double? LCustomEt { get; set; }

    public double? RCustomCt { get; set; }

    public double? LCustomCt { get; set; }

    public string? RCustomPrism { get; set; }

    public string? LCustomPrism { get; set; }

    public int? RFree { get; set; }

    public int? LFree { get; set; }

    public int? SchemeId { get; set; }

    public int? RcancelQty { get; set; }

    public int? LcancelQty { get; set; }

    public string? LOrderNo { get; set; }

    public string? ReadytoSend { get; set; }

    public DateTime? SentonDate { get; set; }

    public DateTime? ReceivedonDate { get; set; }

    public string? Filename { get; set; }

    public string? PartyName { get; set; }

    public string? PartyAddress { get; set; }

    public string? PartyCity { get; set; }

    public string? LocCode { get; set; }

    public decimal? PurchNo { get; set; }

    public string? PartyCustCode { get; set; }

    public decimal? NoOfOrd { get; set; }

    public string? Emailto { get; set; }

    public string? Senderordno { get; set; }

    public string? Sendtolabcode { get; set; }
}
