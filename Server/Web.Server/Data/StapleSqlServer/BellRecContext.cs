using System;
using System.Collections.Generic;
using Bell.Reconciliation.Web.Server.Sqlserver;
using Microsoft.EntityFrameworkCore;

namespace Bell.Reconciliation.Web.Server.Data.Sqlserver;

public partial class BellRecContext : DbContext
{
    public BellRecContext()
    {
    }

    public BellRecContext(DbContextOptions<BellRecContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BellSource> BellSources { get; set; }

    public virtual DbSet<BellUser> BellUsers { get; set; }

    public virtual DbSet<OrderOperation> OrderOperations { get; set; }

    public virtual DbSet<OrderPhoneImei> OrderPhoneImeis { get; set; }

    public virtual DbSet<OrderSnapshot> OrderSnapshots { get; set; }

    public virtual DbSet<RefDealer> RefDealers { get; set; }

    public virtual DbSet<RefJobCode> RefJobCodes { get; set; }

    public virtual DbSet<RefStatusType> RefStatusTypes { get; set; }

    public virtual DbSet<RefStore> RefStores { get; set; }

    public virtual DbSet<ReqResLog> ReqResLogs { get; set; }

    public virtual DbSet<SampleName> SampleNames { get; set; }

    public virtual DbSet<StaplesSource> StaplesSources { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(options => options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BellSource>(entity =>
        {
            entity.ToTable("BellSource", "_data");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Comment).HasMaxLength(250);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.CustomerName).HasMaxLength(250);
            entity.Property(e => e.Imei).HasColumnName("IMEI");
            entity.Property(e => e.Lob)
                .HasMaxLength(250)
                .HasColumnName("LOB");
            entity.Property(e => e.MatchStatus).HasMaxLength(250);
            entity.Property(e => e.RebateType).HasMaxLength(250);
            entity.Property(e => e.Reconciled).HasMaxLength(250);
            entity.Property(e => e.ReconciledBy).HasMaxLength(250);
            entity.Property(e => e.ReconciledDate).HasColumnType("smalldatetime");
            entity.Property(e => e.SubLob).HasMaxLength(250);
            entity.Property(e => e.TransDate).HasColumnType("smalldatetime");
            entity.Property(e => e.TransactionDate).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<BellUser>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK_data.BellUser");

            entity.ToTable("BellUser", "_data");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.Created).HasColumnType("smalldatetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.Updated).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(20);
            entity.Property(e => e.UserId)
                .HasMaxLength(5)
                .HasColumnName("UserID");
        });

        modelBuilder.Entity<OrderOperation>(entity =>
        {
            entity.ToTable("OrderOperation", "_data", tb => tb.HasTrigger("t_OrderOperation_DateTime"));

            entity.HasIndex(e => new { e.MasterId, e.Version }, "IDX_OrderOperation_MasterId_Version");

            entity.HasIndex(e => e.StapleTransactionId, "IDX_OrderOperation_StapleId");

            entity.HasIndex(e => new { e.MasterId, e.Version }, "UC_OrderOperation_BellTxId_Ver").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Action).HasMaxLength(64);
            entity.Property(e => e.ActivityId)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.MasterId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.OriginalTransactionId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StapleTransactionId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.TransactionType).HasMaxLength(32);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrderPhoneImei>(entity =>
        {
            entity.ToTable("OrderPhoneImei", "_data");

            entity.HasIndex(e => e.MasterId, "IDX_OrderPhoneImei_MasterId");

            entity.HasIndex(e => e.Value, "IDX_OrderPhoneImei_Value");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MasterId).HasMaxLength(20);
            entity.Property(e => e.Notes).HasMaxLength(256);
            entity.Property(e => e.Type).HasMaxLength(32);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Value).HasMaxLength(64);
        });

        modelBuilder.Entity<OrderSnapshot>(entity =>
        {
            entity.ToTable("OrderSnapshot", "_data", tb => tb.HasTrigger("t_OrderSnapshot_DateTime"));

            entity.HasIndex(e => new { e.MasterId, e.Version }, "IDX_OrderSnapshot_MasterId_Version");

            entity.HasIndex(e => e.StapleTransactionId, "IDX_OrderSnapshot_StapleId");

            entity.HasIndex(e => new { e.MasterId, e.Version }, "UC_OrderSnapshot_BellTxId_Ver").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActivityId)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.MasterId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.OriginalTransactionId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StapleTransactionId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RefDealer>(entity =>
        {
            entity.HasKey(e => e.DealerCode).HasName("PK_Dealer");

            entity.ToTable("REF_Dealer", "_data");

            entity.Property(e => e.DealerCode).HasMaxLength(5);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");

            entity.HasOne(d => d.StatusType).WithMany(p => p.RefDealers)
                .HasForeignKey(d => d.StatusTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dealer_StatusType");

            entity.HasOne(d => d.Store).WithMany(p => p.RefDealers)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dealer_Store");
        });

        modelBuilder.Entity<RefJobCode>(entity =>
        {
            entity.HasKey(e => e.JobCode).HasName("PK_JobCode");

            entity.ToTable("REF_JobCode", "_data");

            entity.Property(e => e.JobCode).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        modelBuilder.Entity<RefStatusType>(entity =>
        {
            entity.ToTable("REF_StatusType", "_data");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(10);
        });

        modelBuilder.Entity<RefStore>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK_Store");

            entity.ToTable("REF_Store", "_data");

            entity.Property(e => e.StoreId)
                .ValueGeneratedNever()
                .HasColumnName("StoreID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.District).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(6);
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Region).HasMaxLength(100);
            entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");
            entity.Property(e => e.Suite).HasMaxLength(20);

            entity.HasOne(d => d.StatusType).WithMany(p => p.RefStores)
                .HasForeignKey(d => d.StatusTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Store_StatusType");
        });

        modelBuilder.Entity<ReqResLog>(entity =>
        {
            entity.ToTable("ReqResLogs", "_data");

            entity.HasIndex(e => e.ActivityId, "IDX_ReqResLogs_ActId");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActivityId)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Direction)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Method)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.RequestId).HasMaxLength(128);
            entity.Property(e => e.Timestamp).HasColumnType("datetime");
            entity.Property(e => e.Type)
                .HasMaxLength(128)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SampleName>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sampleNames", "_data");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phone1");
            entity.Property(e => e.Phone2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phone2");
            entity.Property(e => e.Postal)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("postal");
            entity.Property(e => e.Province)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("province");
            entity.Property(e => e.Web)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("web");
        });

        modelBuilder.Entity<StaplesSource>(entity =>
        {
            entity.ToTable("StaplesSource", "_data");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Brand).HasMaxLength(250);
            entity.Property(e => e.Comment).HasMaxLength(250);
            entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");
            entity.Property(e => e.CustomerName).HasMaxLength(250);
            entity.Property(e => e.DeviceCo).HasMaxLength(250);
            entity.Property(e => e.Imei).HasColumnName("IMEI");
            entity.Property(e => e.Lob)
                .HasMaxLength(250)
                .HasColumnName("LOB");
            entity.Property(e => e.Location).HasMaxLength(250);
            entity.Property(e => e.Msf).HasColumnName("MSF");
            entity.Property(e => e.Product).HasMaxLength(250);
            entity.Property(e => e.RebateType).HasMaxLength(250);
            entity.Property(e => e.Rec).HasMaxLength(250);
            entity.Property(e => e.Reconciled).HasMaxLength(250);
            entity.Property(e => e.ReconciledBy).HasMaxLength(250);
            entity.Property(e => e.ReconciledDate).HasColumnType("smalldatetime");
            entity.Property(e => e.SalesPerson).HasMaxLength(250);
            entity.Property(e => e.SubLob).HasMaxLength(250);
            entity.Property(e => e.TransactionDate).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
        });

        // OnModelCreatingPartial(modelBuilder);
    }

    //  private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}