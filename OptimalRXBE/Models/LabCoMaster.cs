using System;
using System.Collections.Generic;

namespace OptimalRXBE.Models;

public partial class LabCoMaster
{
    public string LabCode { get; set; } = null!;

    public string? LabId { get; set; }

    public string? LabCoName { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public int? Zip { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public string? GeneralEmail { get; set; }

    public string? Email1 { get; set; }

    public string? Email2 { get; set; }

    public string? WebSite { get; set; }

    public string? ContactPerson { get; set; }

    public string? MobilePhone { get; set; }

    public DateTime? EstablishmentDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? DefaultLab { get; set; }

    public string? Ftpaddress { get; set; }

    public string? Ipaddress { get; set; }

    public string? DialingPhone1 { get; set; }

    public string? DialingPhone2 { get; set; }

    public string? DialingPhone3 { get; set; }

    public string? Bstnumber { get; set; }

    public string? Cstnumber { get; set; }

    public double? SalesTax { get; set; }

    public double? Surcharge { get; set; }

    public double? TurnOverTax { get; set; }

    public string? Loginid { get; set; }

    public string? Password { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Popadd { get; set; }

    public string? Smtpadd { get; set; }

    public decimal? LabPreference { get; set; }

    public string? Inbox { get; set; }

    public string? Outbox { get; set; }

    public string? LocalSql { get; set; }

    public string? LabType { get; set; }

    public string? DialConnName { get; set; }

    public string? DialUserName { get; set; }

    public string? DialPassword { get; set; }

    public string? DialPhone1 { get; set; }

    public string? DialPhone2 { get; set; }

    public decimal? DialTimeDiff { get; set; }

    public string? DialOutboxPath { get; set; }

    public string? DialInboxPath { get; set; }

    public int? Checkmailminutes { get; set; }

    public string? Localonly { get; set; }

    public string? LLabCode { get; set; }

    public string? Popadd2 { get; set; }

    public string? Smtpadd2 { get; set; }

    public string? Userid2 { get; set; }

    public string? Pwd { get; set; }

    public string? Popadd3 { get; set; }

    public string? Smtpadd3 { get; set; }

    public string? Userid3 { get; set; }

    public string? Password3 { get; set; }

    public int? PopServerPort3 { get; set; }

    public int? SmtpServerPort3 { get; set; }

    public string? RxLocCode { get; set; }

    public string? OrgCode { get; set; }

    public string? RxCustAcc { get; set; }

    public string? LocalInvoicePrefix { get; set; }

    public string? InterstateInvoicePrefix { get; set; }

    public string? ExportInvoicePrefix { get; set; }

    public bool? Updateavailable { get; set; }

    public string? Version { get; set; }

    public string? BranchName { get; set; }
}
