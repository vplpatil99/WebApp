using System;
using System.Collections.Generic;

namespace OptimalRXBE.Models;

public partial class Partydetail
{
    public string? PartyCode { get; set; }

    public string PartyName { get; set; } = null!;

    public string? Address { get; set; }

    public string? ContactPerson { get; set; }

    public string? CountryName { get; set; }

    public string? CityName { get; set; }

    public string? Phones { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public double? SalesTax { get; set; }

    public float? TurnoverTax { get; set; }

    public double? Surcharge { get; set; }

    public string? PartyRemarks { get; set; }

    public string? OwnerName { get; set; }

    public string? Area { get; set; }

    public string? State { get; set; }

    public string? Zone { get; set; }

    public string? ZipCode { get; set; }

    public string? TypeOfOptician { get; set; }

    public int? ApproxSales { get; set; }

    public int? SalesRank { get; set; }

    public string? ShopSize { get; set; }

    public string? ShopDecor { get; set; }

    public string? ShopSale { get; set; }

    public string? ShopAreaRank { get; set; }

    public string? Mcode { get; set; }

    public string? MarketingPerson { get; set; }

    public double? PartyDiscount { get; set; }

    public string? PgroupName { get; set; }

    public string? ActivatedYn { get; set; }

    public string? WebSite { get; set; }

    public string? ShopProspective { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UserName { get; set; }

    public string? InvoiceChallan { get; set; }

    public string? SendReport { get; set; }

    public int PartyCounter { get; set; }

    public string? QualityGrade { get; set; }

    public string? ReqPowerPrintout { get; set; }

    public string? RemInvoiceDwm { get; set; }

    public string? MobileMail { get; set; }

    public string? MobileMailAddress { get; set; }

    public string? AutoSendMobileMail { get; set; }

    public int? BillPercentage { get; set; }

    public double? FittingCharge { get; set; }

    public string? AdvanceDepositParty { get; set; }

    public int? CourierCharges { get; set; }

    public int? ForMultipleOfOrders { get; set; }

    public string? Cceno { get; set; }

    public string? Ccename { get; set; }

    public string? InformPendingOrder { get; set; }

    public string? InformPendingOrderMp { get; set; }

    public string? CallForOrderDaily { get; set; }

    public string? CallForOrderByMp { get; set; }

    public DateTime? CallOnTimeForOrder { get; set; }

    public int? CourierWeightInGram { get; set; }

    public int? CourierWtCharges { get; set; }

    public string? MemoryPhoneId { get; set; }

    public string? MemoryPhoneId1 { get; set; }

    public int? MemPhoneNumber { get; set; }

    public int? MemPhoneNumber1 { get; set; }

    public decimal? Phone1 { get; set; }

    public decimal? Phone2 { get; set; }

    public decimal? Phone3 { get; set; }

    public decimal? Phone4 { get; set; }

    public decimal? Phone5 { get; set; }

    public string? PartyGrade { get; set; }

    public string? CreditPolicyApplicable { get; set; }

    public decimal? MaxCrPeriodInDays { get; set; }

    public string? NotShowOutstandingOnChallan { get; set; }

    public string? Cstno { get; set; }

    public string? Lstno { get; set; }

    public string? Panno { get; set; }

    public decimal? Invoicediscount { get; set; }

    public decimal? MaxCrAmountInRs { get; set; }

    public string? Vat { get; set; }

    public string? BillingPartyName { get; set; }

    public string? BillingPartyAddress { get; set; }

    public string? BillingPartyYesNo { get; set; }

    public string? Pwd { get; set; }

    public string? LoginDate { get; set; }

    public string? ReCalcRate { get; set; }

    public string? CurrencyCheck { get; set; }

    public double? ValueInRsForCurrency { get; set; }

    public string? Currency { get; set; }

    public string? ExciseTax { get; set; }

    public string? EducationCess { get; set; }

    public string? HigherEduCess { get; set; }

    public decimal? Potentialsale { get; set; }

    public string? PartyCategory { get; set; }

    public string? LinkwithWholsaler { get; set; }

    public string? GstinUniqueId { get; set; }

    public string? SUtgst { get; set; }

    public string? Cgst { get; set; }

    public string? Igst { get; set; }

    public string? Cess { get; set; }

    public string? Issezlocation { get; set; }

    public string? IslowerGstrate { get; set; }

    public string? Isgovtagency { get; set; }

    public string? ExemptionNotificationNo { get; set; }

    public string? Isrelatedparty { get; set; }

    public decimal Id { get; set; }

    public string? Aadharno { get; set; }

    public string? Vid { get; set; }

    public byte[]? Qrcode { get; set; }

    public DateTime? CreationDateTime { get; set; }

    public string? LegalEntityCode { get; set; }

    public string? CustomerCode { get; set; }

    public double? OrgOpenAr { get; set; }

    public double? OpenAr { get; set; }

    public string? Blocked { get; set; }

    public double? UnInvoicedAmt { get; set; }

    public string? OverDueInvoice { get; set; }

    public string? PaymentTermsCode { get; set; }

    public string? RxCustAcc { get; set; }
}
