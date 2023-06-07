using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bell.Reconciliation.Web.Server.Data.StapleSqlServer;

public partial class StapleContext : DbContext
{
    public StapleContext()
    {
    }

    public StapleContext(DbContextOptions<StapleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BellSource> BellSources { get; set; }

    public virtual DbSet<OrderPhoneImei> OrderPhoneImeis { get; set; }

    public virtual DbSet<SampleName> SampleNames { get; set; }

    public virtual DbSet<StaplesSource> StaplesSources { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=efi-stg-database-smi.77e4410e91a5.database.windows.net;Initial Catalog=BELL_INTEGRATION_DEV;Persist Security Info=True;User ID=log_util_bell;Password=test;");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(options => options.EnableRetryOnFailure());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BellSource>(entity =>
        {
            entity.ToTable("BellSource");

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
            entity.Property(e => e.ReconciledDate).HasMaxLength(250);
            entity.Property(e => e.SubLob).HasMaxLength(250);
            //entity.Property(e => e.TransDate).HasColumnType("smalldatetime");
            // entity.Property(e => e.TransactionDate).HasColumnType("text");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
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
            entity.ToTable("StaplesSource");

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
            entity.Property(e => e.ReconciledDate).HasMaxLength(250);
            entity.Property(e => e.SalesPerson).HasMaxLength(250);
            entity.Property(e => e.SubLob).HasMaxLength(250);
            entity.Property(e => e.TransactionDate).HasColumnType("text");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
        });

        // OnModelCreatingPartial(modelBuilder);
    }

    // private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}