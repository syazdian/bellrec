using System;
using System.Collections.Generic;
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

    public virtual DbSet<BellSourceX> BellSourceXes { get; set; }

    public virtual DbSet<SampleName> SampleNames { get; set; }

    public virtual DbSet<StaplesSource> StaplesSources { get; set; }

    public virtual DbSet<StaplesSourceX> StaplesSourceXes { get; set; }

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
            entity.HasKey(e => e.Id).HasName("PK__BellSour__3214EC077F769EFE");

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
            entity.Property(e => e.ReconciledDate).HasColumnType("smalldatetime");
            entity.Property(e => e.SubLob).HasMaxLength(250);
            entity.Property(e => e.TransactionDate).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<BellSourceX>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BellSource");

            entity.ToTable("BellSourceX");

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
            entity.Property(e => e.TransactionDate).HasColumnType("smalldatetime");
            entity.Property(e => e.UpdateDate).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<SampleName>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sampleNames");

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
            entity.HasKey(e => e.Id).HasName("PK__StaplesS__3214EC0733FE8660");

            entity.ToTable("StaplesSource");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Brand).HasMaxLength(250);
            entity.Property(e => e.Comment).HasMaxLength(250);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
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
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<StaplesSourceX>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_StaplesSource");

            entity.ToTable("StaplesSourceX");

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

        //OnModelCreatingPartial(modelBuilder);
    }

    //private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}