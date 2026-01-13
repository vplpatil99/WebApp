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

    public virtual DbSet<OrderMaster> OrderMasters { get; set; }

    public virtual DbSet<Orderinfo> Orderinfos { get; set; }


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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
