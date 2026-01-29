using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OptimalRXBE.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accessibility> Accessibilities { get; set; }

    public virtual DbSet<CancelledOrder> CancelledOrders { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<LabCoMaster> LabCoMasters { get; set; }

    public virtual DbSet<OrderMaster> OrderMasters { get; set; }

    public virtual DbSet<Orderinfo> Orderinfos { get; set; }

    public virtual DbSet<OriginalOrder> OriginalOrders { get; set; }

    public virtual DbSet<Partydetail> Partydetails { get; set; }

    public virtual DbSet<Stagescompleted> Stagescompleteds { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlServer("Server=10.208.183.20;Database=prime_003_new;User Id=sa;Password=sachin@123;TrustServerCertificate=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accessibility>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACCESSIBILITY");

            entity.Property(e => e.BatchPro).HasMaxLength(3);
            entity.Property(e => e.Branchid).HasColumnName("branchid");
            entity.Property(e => e.Breakage).HasMaxLength(3);
            entity.Property(e => e.FindorderstatusReport)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Invoice).HasMaxLength(3);
            entity.Property(e => e.IsServerRunning).HasColumnName("isServerRunning");
            entity.Property(e => e.Lens).HasMaxLength(3);
            entity.Property(e => e.Master).HasMaxLength(3);
            entity.Property(e => e.Measurements).HasMaxLength(3);
            entity.Property(e => e.MonthEndRep).HasMaxLength(3);
            entity.Property(e => e.Orders).HasMaxLength(3);
            entity.Property(e => e.Passwd).HasMaxLength(20);
            entity.Property(e => e.Payment).HasMaxLength(3);
            entity.Property(e => e.Reports).HasMaxLength(3);
            entity.Property(e => e.Stock).HasMaxLength(3);
            entity.Property(e => e.UserId).ValueGeneratedOnAdd();
            entity.Property(e => e.UserType)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Username).HasMaxLength(20);
            entity.Property(e => e.View).HasMaxLength(3);
        });

        modelBuilder.Entity<CancelledOrder>(entity =>
        {
            entity.HasIndex(e => e.PartyCode, "IX_CancelledOrders").HasFillFactor(90);

            entity.HasIndex(e => new { e.Lqty, e.Rqty }, "IX_CancelledOrders_1").HasFillFactor(90);

            entity.HasIndex(e => e.Category, "IX_CancelledOrders_2").HasFillFactor(90);

            entity.HasIndex(e => e.GOrderNo, "ncGno")
                .IsDescending()
                .HasFillFactor(90);

            entity.HasIndex(e => e.UpdateDate, "ncUpdt").HasFillFactor(90);

            entity.Property(e => e.Category).HasMaxLength(2);
            entity.Property(e => e.CreditNoteNo).HasMaxLength(20);
            entity.Property(e => e.GOrderNo)
                .HasMaxLength(20)
                .HasColumnName("G_orderNo");
            entity.Property(e => e.Lqty).HasColumnName("LQty");
            entity.Property(e => e.OrderNo)
                .HasMaxLength(20)
                .HasColumnName("Order_No");
            entity.Property(e => e.PartyCode)
                .HasMaxLength(75)
                .HasColumnName("Party_Code");
            entity.Property(e => e.Reason).HasMaxLength(100);
            entity.Property(e => e.Remarks).HasMaxLength(100);
            entity.Property(e => e.Rqty).HasColumnName("RQty");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(25);
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("Delivery", tb => tb.HasTrigger("UpdateDeliveryTime"));

            entity.HasIndex(e => new { e.GOrderNo, e.Ddate, e.Dtime }, "IX_Delivery")
                .IsDescending()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.DCcode, e.ChallanNo, e.ChallanDate }, "IX_Delivery_1")
                .IsDescending(false, true, true)
                .HasFillFactor(90);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("id");
            entity.Property(e => e.ChallanDate).HasColumnType("datetime");
            entity.Property(e => e.ChallanNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ConsDateTime).HasColumnType("datetime");
            entity.Property(e => e.ConsignmentNo)
                .HasMaxLength(20)
                .HasColumnName("consignmentNo");
            entity.Property(e => e.DCcode).HasColumnName("D_CCode");
            entity.Property(e => e.Ddate)
                .HasColumnType("datetime")
                .HasColumnName("DDate");
            entity.Property(e => e.Dtime)
                .HasColumnType("datetime")
                .HasColumnName("DTime");
            entity.Property(e => e.GOrderNo)
                .HasMaxLength(20)
                .HasColumnName("G_OrderNo");
            entity.Property(e => e.ModeofDel)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("modeofDel");
        });

        modelBuilder.Entity<LabCoMaster>(entity =>
        {
            entity.HasKey(e => e.LabCode).HasFillFactor(90);

            entity.ToTable("LabCoMaster");

            entity.HasIndex(e => e.LabCode, "IX_LabCoMaster").HasFillFactor(90);

            entity.HasIndex(e => e.LabId, "IX_LabCoMaster_1").HasFillFactor(90);

            entity.Property(e => e.LabCode).HasMaxLength(3);
            entity.Property(e => e.Address).HasMaxLength(150);
            entity.Property(e => e.BranchName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Bstnumber)
                .HasMaxLength(25)
                .HasColumnName("BSTNUMBER");
            entity.Property(e => e.Checkmailminutes).HasColumnName("checkmailminutes");
            entity.Property(e => e.City).HasMaxLength(25);
            entity.Property(e => e.ContactPerson).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(25);
            entity.Property(e => e.Cstnumber)
                .HasMaxLength(25)
                .HasColumnName("CSTNUMBER");
            entity.Property(e => e.DefaultLab)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.DialConnName).HasMaxLength(25);
            entity.Property(e => e.DialInboxPath).HasMaxLength(250);
            entity.Property(e => e.DialOutboxPath).HasMaxLength(250);
            entity.Property(e => e.DialPassword).HasMaxLength(25);
            entity.Property(e => e.DialPhone1).HasMaxLength(25);
            entity.Property(e => e.DialPhone2).HasMaxLength(25);
            entity.Property(e => e.DialTimeDiff).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.DialUserName).HasMaxLength(25);
            entity.Property(e => e.DialingPhone1).HasMaxLength(25);
            entity.Property(e => e.DialingPhone2).HasMaxLength(25);
            entity.Property(e => e.DialingPhone3).HasMaxLength(25);
            entity.Property(e => e.Email1).HasMaxLength(50);
            entity.Property(e => e.Email2).HasMaxLength(50);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.EstablishmentDate).HasColumnType("datetime");
            entity.Property(e => e.ExportInvoicePrefix)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("export_invoice_prefix");
            entity.Property(e => e.Fax).HasMaxLength(25);
            entity.Property(e => e.Ftpaddress)
                .HasMaxLength(50)
                .HasColumnName("FTPAddress");
            entity.Property(e => e.GeneralEmail).HasMaxLength(50);
            entity.Property(e => e.Inbox).HasMaxLength(200);
            entity.Property(e => e.InterstateInvoicePrefix)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("interstate_invoice_prefix");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(25)
                .HasColumnName("IPAddress");
            entity.Property(e => e.LLabCode)
                .HasMaxLength(50)
                .HasColumnName("lLabCode");
            entity.Property(e => e.LabCoName).HasMaxLength(50);
            entity.Property(e => e.LabId).HasMaxLength(10);
            entity.Property(e => e.LabPreference).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.LabType).HasMaxLength(10);
            entity.Property(e => e.LocalInvoicePrefix)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("local_invoice_prefix");
            entity.Property(e => e.LocalSql)
                .HasMaxLength(50)
                .HasColumnName("LocalSQL");
            entity.Property(e => e.Localonly)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("localonly");
            entity.Property(e => e.Loginid)
                .HasMaxLength(100)
                .HasColumnName("loginid");
            entity.Property(e => e.MobilePhone).HasMaxLength(25);
            entity.Property(e => e.OrgCode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("ORG_CODE");
            entity.Property(e => e.Outbox).HasMaxLength(200);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
            entity.Property(e => e.Password3)
                .HasMaxLength(10)
                .HasColumnName("password3");
            entity.Property(e => e.Phone).HasMaxLength(25);
            entity.Property(e => e.PopServerPort3).HasColumnName("popServerPort3");
            entity.Property(e => e.Popadd)
                .HasMaxLength(100)
                .HasColumnName("popadd");
            entity.Property(e => e.Popadd2)
                .HasMaxLength(100)
                .HasColumnName("popadd2");
            entity.Property(e => e.Popadd3)
                .HasMaxLength(20)
                .HasColumnName("popadd3");
            entity.Property(e => e.Pwd)
                .HasMaxLength(50)
                .HasColumnName("pwd");
            entity.Property(e => e.RxCustAcc)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("RX_CUST_ACC");
            entity.Property(e => e.RxLocCode)
                .HasMaxLength(24)
                .IsUnicode(false)
                .HasColumnName("RX_LOC_CODE");
            entity.Property(e => e.SmtpServerPort3).HasColumnName("smtpServerPort3");
            entity.Property(e => e.Smtpadd)
                .HasMaxLength(100)
                .HasColumnName("smtpadd");
            entity.Property(e => e.Smtpadd2)
                .HasMaxLength(100)
                .HasColumnName("smtpadd2");
            entity.Property(e => e.Smtpadd3)
                .HasMaxLength(20)
                .HasColumnName("smtpadd3");
            entity.Property(e => e.State).HasMaxLength(25);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Updateavailable).HasColumnName("updateavailable");
            entity.Property(e => e.Userid2)
                .HasMaxLength(100)
                .HasColumnName("userid2");
            entity.Property(e => e.Userid3)
                .HasMaxLength(30)
                .HasColumnName("userid3");
            entity.Property(e => e.Version)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("version");
            entity.Property(e => e.WebSite).HasMaxLength(50);
        });

        modelBuilder.Entity<OrderMaster>(entity =>
        {
            entity.HasKey(e => e.GOrderNo).HasName("PK_OrderMaster_12July");

            entity.ToTable("OrderMaster", tb =>
                {
                    tb.HasTrigger("NvgRevertLenstypeOnCompleteOrder");
                    tb.HasTrigger("trg_UpdateFinishedLensSource");
                });

            entity.HasIndex(e => new { e.OrderDate, e.OrderEntryTime }, "OrdDate_NonClusteredIndex-20200929").IsDescending();

            entity.HasIndex(e => new { e.PartyCode, e.OrderNo }, "PCodeOrdNo_NonClusteredIndex-20200929");

            entity.HasIndex(e => e.LOrderNo, "UniqueL_ordernoConstraint").IsUnique();

            entity.Property(e => e.GOrderNo)
                .HasMaxLength(20)
                .HasColumnName("G_orderNo");
            entity.Property(e => e.ChallanDate).HasColumnType("datetime");
            entity.Property(e => e.ChallanNo).HasMaxLength(20);
            entity.Property(e => e.CoatColor).HasMaxLength(30);
            entity.Property(e => e.Coating).HasMaxLength(30);
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.Emailto)
                .HasMaxLength(250)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("emailto");
            entity.Property(e => e.Filename)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FinishedLensSource).HasMaxLength(75);
            entity.Property(e => e.Fitting).HasMaxLength(30);
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            entity.Property(e => e.InvoiceNo).HasMaxLength(30);
            entity.Property(e => e.LAbase).HasColumnName("L_ABase");
            entity.Property(e => e.LCt)
                .HasMaxLength(10)
                .HasColumnName("L_CT");
            entity.Property(e => e.LCustomBase)
                .HasMaxLength(5)
                .HasColumnName("L_CustomBase");
            entity.Property(e => e.LCustomCt).HasColumnName("L_CustomCT");
            entity.Property(e => e.LCustomEt).HasColumnName("L_CustomET");
            entity.Property(e => e.LCustomPrism)
                .HasMaxLength(10)
                .HasColumnName("L_CustomPrism");
            entity.Property(e => e.LEt)
                .HasMaxLength(10)
                .HasColumnName("L_ET");
            entity.Property(e => e.LFree).HasColumnName("L_Free");
            entity.Property(e => e.LManual)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("L_Manual");
            entity.Property(e => e.LOrderNo)
                .HasMaxLength(20)
                .HasColumnName("L_OrderNo");
            entity.Property(e => e.Laddn)
                .HasMaxLength(6)
                .HasColumnName("LAddn");
            entity.Property(e => e.Laxis)
                .HasMaxLength(6)
                .HasColumnName("LAxis");
            entity.Property(e => e.Lbase)
                .HasMaxLength(5)
                .HasColumnName("LBase");
            entity.Property(e => e.LbaseCurve).HasColumnName("LBaseCurve");
            entity.Property(e => e.LbaseSag).HasColumnName("LBaseSag");
            entity.Property(e => e.LblankBrand)
                .HasMaxLength(20)
                .HasColumnName("LBlankBrand");
            entity.Property(e => e.LblankSupplier)
                .HasMaxLength(50)
                .HasColumnName("LBlankSupplier");
            entity.Property(e => e.LcancelQty).HasColumnName("LCancelQty");
            entity.Property(e => e.Lcyl)
                .HasMaxLength(6)
                .HasColumnName("LCyl");
            entity.Property(e => e.Lcylsag).HasColumnName("LCYLSag");
            entity.Property(e => e.Ldiscount).HasColumnName("LDiscount");
            entity.Property(e => e.LensFrameBoth)
                .HasMaxLength(6)
                .HasColumnName("Lens_Frame_Both");
            entity.Property(e => e.LensType)
                .HasMaxLength(75)
                .HasColumnName("Lens_Type");
            entity.Property(e => e.LfinalCt).HasColumnName("LFinalCT");
            entity.Property(e => e.LfinalEt).HasColumnName("LFinalET");
            entity.Property(e => e.LfinishedStkTakenUc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("LFinishedStkTakenUC");
            entity.Property(e => e.LocCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Loc_Code");
            entity.Property(e => e.Lprism)
                .HasMaxLength(10)
                .HasColumnName("LPrism");
            entity.Property(e => e.Lprocessed)
                .HasMaxLength(1)
                .HasColumnName("LProcessed");
            entity.Property(e => e.Lqty)
                .HasMaxLength(2)
                .HasColumnName("LQty");
            entity.Property(e => e.Lrate).HasColumnName("LRate");
            entity.Property(e => e.Lsph)
                .HasMaxLength(6)
                .HasColumnName("LSph");
            entity.Property(e => e.Lsphsag).HasColumnName("LSPHSag");
            entity.Property(e => e.Lstock)
                .HasMaxLength(1)
                .HasColumnName("LStock");
            entity.Property(e => e.Ltc).HasColumnName("LTC");
            entity.Property(e => e.Ltool)
                .HasMaxLength(25)
                .HasColumnName("LTool");
            entity.Property(e => e.LtoolsFound).HasColumnName("LToolsFound");
            entity.Property(e => e.MustGoCoatDate).HasColumnType("datetime");
            entity.Property(e => e.MustGoCoatTime).HasColumnType("datetime");
            entity.Property(e => e.MustGoOrderDate).HasColumnType("datetime");
            entity.Property(e => e.MustGoOrderTime).HasColumnType("datetime");
            entity.Property(e => e.NoOfOrd)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("no_of_ord");
            entity.Property(e => e.ONumber)
                .HasMaxLength(20)
                .HasColumnName("O_Number");
            entity.Property(e => e.OrdRecievdBy).HasMaxLength(50);
            entity.Property(e => e.OrderCompleteDateTime).HasColumnType("datetime");
            entity.Property(e => e.OrderD)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Order_D");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("Order_Date");
            entity.Property(e => e.OrderEntryTime).HasColumnType("datetime");
            entity.Property(e => e.OrderNo)
                .HasMaxLength(20)
                .HasColumnName("Order_No");
            entity.Property(e => e.PartyAddress)
                .HasMaxLength(250)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Party_address");
            entity.Property(e => e.PartyCity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Party_city");
            entity.Property(e => e.PartyCode)
                .HasMaxLength(75)
                .HasColumnName("Party_Code");
            entity.Property(e => e.PartyCustCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("party_cust_code");
            entity.Property(e => e.PartyName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Party_name");
            entity.Property(e => e.PartyOrderRefNo)
                .HasMaxLength(30)
                .HasColumnName("Party_OrderRefNo");
            entity.Property(e => e.PartyPlace).HasMaxLength(25);
            entity.Property(e => e.PrecisionOrder)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Precision_Order");
            entity.Property(e => e.Printed).HasMaxLength(4);
            entity.Property(e => e.Processed).HasMaxLength(1);
            entity.Property(e => e.PurchNo).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.RAbase).HasColumnName("R_ABase");
            entity.Property(e => e.RCt)
                .HasMaxLength(10)
                .HasColumnName("R_CT");
            entity.Property(e => e.RCustomBase)
                .HasMaxLength(5)
                .HasColumnName("R_CustomBase");
            entity.Property(e => e.RCustomCt).HasColumnName("R_CustomCT");
            entity.Property(e => e.RCustomEt).HasColumnName("R_CustomET");
            entity.Property(e => e.RCustomPrism)
                .HasMaxLength(10)
                .HasColumnName("R_CustomPrism");
            entity.Property(e => e.REt)
                .HasMaxLength(10)
                .HasColumnName("R_ET");
            entity.Property(e => e.RFree).HasColumnName("R_Free");
            entity.Property(e => e.RManual)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("R_Manual");
            entity.Property(e => e.Raddn)
                .HasMaxLength(6)
                .HasColumnName("RAddn");
            entity.Property(e => e.Raxis)
                .HasMaxLength(10)
                .HasColumnName("RAxis");
            entity.Property(e => e.Rbase)
                .HasMaxLength(5)
                .HasColumnName("RBase");
            entity.Property(e => e.RbaseCurve).HasColumnName("RBaseCurve");
            entity.Property(e => e.RbaseSag).HasColumnName("RBaseSag");
            entity.Property(e => e.RblankBrand)
                .HasMaxLength(20)
                .HasColumnName("RBlankBrand");
            entity.Property(e => e.RblankSupplier)
                .HasMaxLength(50)
                .HasColumnName("RBlankSupplier");
            entity.Property(e => e.RcancelQty).HasColumnName("RCancelQty");
            entity.Property(e => e.Rcyl)
                .HasMaxLength(6)
                .HasColumnName("RCyl");
            entity.Property(e => e.Rcylsag).HasColumnName("RCYLSag");
            entity.Property(e => e.Rdiscount).HasColumnName("RDiscount");
            entity.Property(e => e.ReadytoSend)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ReceivedonDate).HasColumnType("datetime");
            entity.Property(e => e.RegisterNo).HasMaxLength(30);
            entity.Property(e => e.Remarks).HasMaxLength(250);
            entity.Property(e => e.RfinalCt).HasColumnName("RFinalCT");
            entity.Property(e => e.RfinalEt).HasColumnName("RFinalET");
            entity.Property(e => e.RfinishedStkTakenUc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RFinishedStkTakenUC");
            entity.Property(e => e.Rprism)
                .HasMaxLength(10)
                .HasColumnName("RPrism");
            entity.Property(e => e.Rprocessed)
                .HasMaxLength(1)
                .HasColumnName("RProcessed");
            entity.Property(e => e.Rqty)
                .HasMaxLength(2)
                .HasColumnName("RQty");
            entity.Property(e => e.Rrate).HasColumnName("RRate");
            entity.Property(e => e.Rsph)
                .HasMaxLength(6)
                .HasColumnName("RSph");
            entity.Property(e => e.Rsphsag).HasColumnName("RSPHSag");
            entity.Property(e => e.Rstock)
                .HasMaxLength(1)
                .HasColumnName("RStock");
            entity.Property(e => e.Rtc).HasColumnName("RTC");
            entity.Property(e => e.Rtool)
                .HasMaxLength(25)
                .HasColumnName("RTool");
            entity.Property(e => e.RtoolsFound).HasColumnName("RToolsFound");
            entity.Property(e => e.SchemeId).HasColumnName("SchemeID");
            entity.Property(e => e.Senderordno)
                .HasMaxLength(20)
                .HasColumnName("senderordno");
            entity.Property(e => e.Sendtolabcode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sendtolabcode");
            entity.Property(e => e.SentonDate)
                .HasColumnType("datetime")
                .HasColumnName("sentonDate");
            entity.Property(e => e.SingleVisionMultiFocal)
                .HasMaxLength(2)
                .HasColumnName("SingleVision_MultiFocal");
            entity.Property(e => e.Status).HasMaxLength(2);
            entity.Property(e => e.StockOrder)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("stockOrder");
            entity.Property(e => e.TintColor).HasMaxLength(40);
            entity.Property(e => e.ToolRemarks).HasMaxLength(100);
            entity.Property(e => e.TrayColor).HasMaxLength(20);
            entity.Property(e => e.UrgentOrder).HasMaxLength(1);
            entity.Property(e => e.UserName).HasMaxLength(20);
        });

        modelBuilder.Entity<Orderinfo>(entity =>
        {
            entity.HasKey(e => e.GOrderno)
                .HasName("PK__orderinfo__3EE001BF")
                .HasFillFactor(90);

            entity.ToTable("orderinfo");

            entity.HasIndex(e => e.CurrentStage, "IX_orderinfo").HasFillFactor(90);

            entity.HasIndex(e => e.TrayColor, "IX_orderinfo_1").HasFillFactor(90);

            entity.HasIndex(e => new { e.GOrderno, e.CurrentStage }, "_dta_index_orderinfo_8_1262868644__K2_K3").HasFillFactor(90);

            entity.HasIndex(e => e.Id, "ncID")
                .IsDescending()
                .HasFillFactor(90);

            entity.HasIndex(e => e.StockTransferDate, "ncSTDt").HasFillFactor(90);

            entity.HasIndex(e => e.StockTransferNo, "ncSTNo").HasFillFactor(90);

            entity.Property(e => e.GOrderno)
                .HasMaxLength(20)
                .HasColumnName("G_orderno");
            entity.Property(e => e.ActualLensName).HasMaxLength(75);
            entity.Property(e => e.AwbNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Bvd)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("BVD");
            entity.Property(e => e.CurrentStage).HasMaxLength(100);
            entity.Property(e => e.DeliveryStatusSend)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FittingHeight)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FoundLtc)
                .HasMaxLength(5)
                .HasColumnName("Found_LTC");
            entity.Property(e => e.FoundRtc)
                .HasMaxLength(5)
                .HasColumnName("Found_RTC");
            entity.Property(e => e.Glpl)
                .HasMaxLength(1)
                .HasColumnName("GLPL");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("id");
            entity.Property(e => e.Lpd)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LtrueCurve)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("LTrueCurve");
            entity.Property(e => e.OrderDurationHour).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PentaFit)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Processedby)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("processedby");
            entity.Property(e => e.Processedma)
                .HasMaxLength(50)
                .HasColumnName("processedma");
            entity.Property(e => e.Readytosend)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("Y", "DF_orderinfo_readytosend")
                .HasColumnName("readytosend");
            entity.Property(e => e.Rpd)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RtrueCurve)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("RTrueCurve");
            entity.Property(e => e.SendUpdateStage)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Stagenumber).HasColumnName("STAGENUMBER");
            entity.Property(e => e.StockTransferDate).HasColumnType("datetime");
            entity.Property(e => e.StockTransferNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TrayColor).HasMaxLength(20);
            entity.Property(e => e.TrayNo).HasMaxLength(10);
            entity.Property(e => e.WrapAngle)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OriginalOrder>(entity =>
        {
            entity.HasKey(e => e.GOrderNo)
                .HasName("PK__originalorder__09AD1F71")
                .HasFillFactor(90);

            entity.ToTable("OriginalOrder", tb => tb.HasTrigger("DeleteOldCancelled"));

            entity.HasIndex(e => e.OrderDate, "IX_OriginalOrder")
                .IsDescending()
                .HasFillFactor(90);

            entity.Property(e => e.GOrderNo)
                .HasMaxLength(20)
                .HasColumnName("G_orderNo");
            entity.Property(e => e.CoatColor).HasMaxLength(20);
            entity.Property(e => e.Coating).HasMaxLength(7);
            entity.Property(e => e.DummyLaddn)
                .HasMaxLength(10)
                .HasColumnName("DummyLAddn");
            entity.Property(e => e.DummyLaxis)
                .HasMaxLength(6)
                .HasColumnName("DummyLAxis");
            entity.Property(e => e.DummyLcyl)
                .HasMaxLength(6)
                .HasColumnName("DummyLCyl");
            entity.Property(e => e.DummyLensType)
                .HasMaxLength(75)
                .HasColumnName("DummyLens_Type");
            entity.Property(e => e.DummyLsph)
                .HasMaxLength(6)
                .HasColumnName("DummyLSph");
            entity.Property(e => e.DummyRaddn)
                .HasMaxLength(10)
                .HasColumnName("DummyRAddn");
            entity.Property(e => e.DummyRaxis)
                .HasMaxLength(10)
                .HasColumnName("DummyRAxis");
            entity.Property(e => e.DummyRcyl)
                .HasMaxLength(6)
                .HasColumnName("DummyRCyl");
            entity.Property(e => e.DummyRsph)
                .HasMaxLength(6)
                .HasColumnName("DummyRSph");
            entity.Property(e => e.Fitting).HasMaxLength(1);
            entity.Property(e => e.Laddn)
                .HasMaxLength(10)
                .HasColumnName("LAddn");
            entity.Property(e => e.Laxis)
                .HasMaxLength(10)
                .HasColumnName("LAxis");
            entity.Property(e => e.Lcyl)
                .HasMaxLength(10)
                .HasColumnName("LCyl");
            entity.Property(e => e.Ldiscount).HasColumnName("LDiscount");
            entity.Property(e => e.LensFrameBoth)
                .HasMaxLength(2)
                .HasColumnName("Lens_Frame_Both");
            entity.Property(e => e.LensType)
                .HasMaxLength(75)
                .HasColumnName("Lens_Type");
            entity.Property(e => e.Lqty)
                .HasMaxLength(10)
                .HasDefaultValueSql("(0)", "DF_OriginalOrder_LQty")
                .HasColumnName("LQty");
            entity.Property(e => e.Lrate)
                .HasDefaultValue(0.0, "DF_OriginalOrder_LRate")
                .HasColumnName("LRate");
            entity.Property(e => e.Lsph)
                .HasMaxLength(10)
                .HasColumnName("LSph");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("Order_Date");
            entity.Property(e => e.OrderEntryTime).HasColumnType("datetime");
            entity.Property(e => e.OrderNo)
                .HasMaxLength(20)
                .HasColumnName("Order_No");
            entity.Property(e => e.PartyCode)
                .HasMaxLength(75)
                .HasColumnName("Party_Code");
            entity.Property(e => e.PartyCustomerName).HasMaxLength(100);
            entity.Property(e => e.PartyOrderRefNo)
                .HasMaxLength(30)
                .HasColumnName("Party_OrderRefNo");
            entity.Property(e => e.Raddn)
                .HasMaxLength(10)
                .HasColumnName("RAddn");
            entity.Property(e => e.Raxis)
                .HasMaxLength(10)
                .HasColumnName("RAxis");
            entity.Property(e => e.Rcyl)
                .HasMaxLength(10)
                .HasColumnName("RCyl");
            entity.Property(e => e.Rdiscount).HasColumnName("RDiscount");
            entity.Property(e => e.RegisterNo).HasMaxLength(30);
            entity.Property(e => e.Remarks).HasMaxLength(250);
            entity.Property(e => e.Rqty)
                .HasMaxLength(10)
                .HasDefaultValueSql("(0)", "DF_OriginalOrder_RQty")
                .HasColumnName("RQty");
            entity.Property(e => e.Rrate)
                .HasDefaultValue(0.0, "DF_OriginalOrder_RRate")
                .HasColumnName("RRate");
            entity.Property(e => e.Rsph)
                .HasMaxLength(10)
                .HasColumnName("RSph");
            entity.Property(e => e.Senderordno)
                .HasMaxLength(20)
                .HasColumnName("senderordno");
            entity.Property(e => e.Sendtolabcode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sendtolabcode");
            entity.Property(e => e.SingleVisionMultiFocal)
                .HasMaxLength(20)
                .HasColumnName("SingleVision_MultiFocal");
            entity.Property(e => e.Stock).HasMaxLength(6);
            entity.Property(e => e.TintColor).HasMaxLength(40);
        });

        modelBuilder.Entity<Partydetail>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("PARTYDETAILS", tb =>
                {
                    tb.HasTrigger("TRIG__Update_PARTYDETAILS");
                    tb.HasTrigger("TRIG__Update_PARTYDETAILS_Credit_Policy");
                });

            entity.HasIndex(e => e.Id, "ClusteredIndex-20221108-110319").IsClustered();

            entity.HasIndex(e => new { e.PartyCode, e.TypeOfOptician }, "IX_PartyDetails_Code_Type");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("id");
            entity.Property(e => e.Aadharno).HasMaxLength(20);
            entity.Property(e => e.ActivatedYn)
                .HasMaxLength(1)
                .HasColumnName("ActivatedYN");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.AdvanceDepositParty)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Area).HasMaxLength(30);
            entity.Property(e => e.AutoSendMobileMail).HasMaxLength(1);
            entity.Property(e => e.BillingPartyAddress).HasMaxLength(350);
            entity.Property(e => e.BillingPartyName).HasMaxLength(50);
            entity.Property(e => e.BillingPartyYesNo).HasMaxLength(1);
            entity.Property(e => e.Blocked)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BLOCKED");
            entity.Property(e => e.CallForOrderByMp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CallForOrderByMP");
            entity.Property(e => e.CallForOrderDaily)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CallOnTimeForOrder).HasColumnType("datetime");
            entity.Property(e => e.Ccename)
                .HasMaxLength(50)
                .HasColumnName("CCEName");
            entity.Property(e => e.Cceno)
                .HasMaxLength(10)
                .HasColumnName("CCENo");
            entity.Property(e => e.Cess).HasMaxLength(5);
            entity.Property(e => e.Cgst)
                .HasMaxLength(5)
                .HasColumnName("CGST");
            entity.Property(e => e.CityName)
                .HasMaxLength(30)
                .HasColumnName("CITY_NAME");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(50)
                .HasColumnName("CONTACT_PERSON");
            entity.Property(e => e.CountryName)
                .HasMaxLength(30)
                .HasColumnName("COUNTRY_NAME");
            entity.Property(e => e.CreationDateTime)
                .HasDefaultValueSql("(getdate())", "DF_CREATION_DATE_TIME")
                .HasColumnType("datetime")
                .HasColumnName("CREATION_DATE_TIME");
            entity.Property(e => e.CreditPolicyApplicable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Cstno)
                .HasMaxLength(50)
                .HasColumnName("cstno");
            entity.Property(e => e.Currency).HasMaxLength(20);
            entity.Property(e => e.CurrencyCheck)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Currency_Check");
            entity.Property(e => e.CustomerCode)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_CODE");
            entity.Property(e => e.EducationCess).HasMaxLength(2);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.ExciseTax).HasMaxLength(2);
            entity.Property(e => e.ExemptionNotificationNo)
                .HasMaxLength(20)
                .HasColumnName("Exemption_notification_no");
            entity.Property(e => e.Fax)
                .HasMaxLength(25)
                .HasColumnName("FAX");
            entity.Property(e => e.GstinUniqueId)
                .HasMaxLength(20)
                .HasColumnName("GSTIN_UniqueID");
            entity.Property(e => e.HigherEduCess).HasMaxLength(2);
            entity.Property(e => e.Igst)
                .HasMaxLength(5)
                .HasColumnName("IGST");
            entity.Property(e => e.InformPendingOrder)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.InformPendingOrderMp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("InformPendingOrderMP");
            entity.Property(e => e.InvoiceChallan)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Invoice_challan");
            entity.Property(e => e.Invoicediscount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("invoicediscount");
            entity.Property(e => e.Isgovtagency)
                .HasMaxLength(1)
                .HasColumnName("isgovtagency");
            entity.Property(e => e.IslowerGstrate)
                .HasMaxLength(1)
                .HasColumnName("islowerGSTrate");
            entity.Property(e => e.Isrelatedparty)
                .HasMaxLength(1)
                .HasColumnName("isrelatedparty");
            entity.Property(e => e.Issezlocation)
                .HasMaxLength(1)
                .HasColumnName("issezlocation");
            entity.Property(e => e.LegalEntityCode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("LEGAL_ENTITY_CODE");
            entity.Property(e => e.LinkwithWholsaler).HasMaxLength(15);
            entity.Property(e => e.LoginDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Login_Date");
            entity.Property(e => e.Lstno)
                .HasMaxLength(50)
                .HasColumnName("lstno");
            entity.Property(e => e.MarketingPerson).HasMaxLength(30);
            entity.Property(e => e.MaxCrAmountInRs).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.MaxCrPeriodInDays).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Mcode)
                .HasMaxLength(10)
                .HasColumnName("MCode");
            entity.Property(e => e.MemoryPhoneId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MemoryPhoneID");
            entity.Property(e => e.MemoryPhoneId1)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MemoryPhoneID1");
            entity.Property(e => e.MobileMail).HasMaxLength(1);
            entity.Property(e => e.MobileMailAddress).HasMaxLength(50);
            entity.Property(e => e.NotShowOutstandingOnChallan).HasMaxLength(1);
            entity.Property(e => e.OpenAr).HasColumnName("OPEN_AR");
            entity.Property(e => e.OrgOpenAr).HasColumnName("ORG_OPEN_AR");
            entity.Property(e => e.OverDueInvoice)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("OVER_DUE_INVOICE");
            entity.Property(e => e.OwnerName).HasMaxLength(30);
            entity.Property(e => e.Panno)
                .HasMaxLength(50)
                .HasColumnName("panno");
            entity.Property(e => e.PartyCategory)
                .HasMaxLength(25)
                .HasColumnName("partyCategory");
            entity.Property(e => e.PartyCode)
                .HasMaxLength(75)
                .HasColumnName("PARTY_CODE");
            entity.Property(e => e.PartyGrade)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PartyName)
                .HasMaxLength(50)
                .HasColumnName("PARTY_NAME");
            entity.Property(e => e.PartyRemarks)
                .HasMaxLength(250)
                .HasColumnName("PARTY_REMARKS");
            entity.Property(e => e.PaymentTermsCode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("PAYMENT_TERMS_CODE");
            entity.Property(e => e.PgroupName)
                .HasMaxLength(50)
                .HasColumnName("PGroupName");
            entity.Property(e => e.Phone1).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Phone2).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Phone3).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Phone4).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Phone5).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Phones)
                .HasMaxLength(60)
                .HasColumnName("PHONES");
            entity.Property(e => e.Potentialsale).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Pwd)
                .HasMaxLength(20)
                .HasColumnName("PWD");
            entity.Property(e => e.Qrcode).HasColumnName("QRCode");
            entity.Property(e => e.QualityGrade).HasMaxLength(10);
            entity.Property(e => e.ReCalcRate).HasMaxLength(1);
            entity.Property(e => e.RemInvoiceDwm)
                .HasMaxLength(10)
                .HasColumnName("RemInvoiceDWM");
            entity.Property(e => e.ReqPowerPrintout).HasMaxLength(10);
            entity.Property(e => e.RxCustAcc)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("RX_CUST_ACC");
            entity.Property(e => e.SUtgst)
                .HasMaxLength(5)
                .HasColumnName("S_UTGST");
            entity.Property(e => e.SendReport)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ShopAreaRank).HasMaxLength(10);
            entity.Property(e => e.ShopDecor).HasMaxLength(10);
            entity.Property(e => e.ShopProspective).HasMaxLength(10);
            entity.Property(e => e.ShopSale).HasMaxLength(10);
            entity.Property(e => e.ShopSize).HasMaxLength(10);
            entity.Property(e => e.State).HasMaxLength(30);
            entity.Property(e => e.TypeOfOptician).HasMaxLength(1);
            entity.Property(e => e.UnInvoicedAmt).HasColumnName("UN_INVOICED_AMT");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(25);
            entity.Property(e => e.Vat)
                .HasMaxLength(50)
                .HasColumnName("VAT");
            entity.Property(e => e.Vid)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("VID");
            entity.Property(e => e.WebSite).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(15);
            entity.Property(e => e.Zone).HasMaxLength(30);
        });

        modelBuilder.Entity<Stagescompleted>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("stagescompleted", tb =>
                {
                    tb.HasTrigger("TrigOrderStatus1");
                    tb.HasTrigger("trgTransferStagesDataUp");
                });

            entity.HasIndex(e => new { e.Id, e.CompletionTime }, "IX_stagescompleted")
                .IsDescending()
                .HasFillFactor(90);

            entity.HasIndex(e => e.GOrderno, "IX_stagescompleted_3")
                .IsDescending()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.CompletionDate, e.CompletionTime, e.Qty, e.StageName }, "IX_stagescompleted_4")
                .IsDescending(true, true, false, false)
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.CompletionDate, e.CompletionTime, e.UserName, e.StageNumber, e.StageName }, "IX_stagescompleted_5")
                .IsDescending(true, true, false, false, false)
                .HasFillFactor(90);

            entity.HasIndex(e => e.StageName, "IX_stagescompleted_8").HasFillFactor(90);

            entity.HasIndex(e => new { e.LensType, e.GOrderno }, "NonClusteredIndex-20250106-144532").IsDescending(false, true);

            entity.HasIndex(e => new { e.GOrderno, e.StageName, e.CompletionDate, e.LensType }, "entered_order_for_scheme");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("id");
            entity.Property(e => e.CompletionDate).HasColumnType("datetime");
            entity.Property(e => e.CompletionTime).HasColumnType("datetime");
            entity.Property(e => e.GOrderno)
                .HasMaxLength(20)
                .HasColumnName("G_orderno");
            entity.Property(e => e.LabNo).HasMaxLength(20);
            entity.Property(e => e.LensType)
                .HasMaxLength(75)
                .HasColumnName("Lens_type");
            entity.Property(e => e.LohPort).HasMaxLength(7);
            entity.Property(e => e.StageName).HasMaxLength(100);
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .HasColumnName("userName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
