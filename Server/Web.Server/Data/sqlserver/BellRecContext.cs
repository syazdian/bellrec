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

    public virtual DbSet<SampleName> SampleNames { get; set; }

    public virtual DbSet<StaplesSource> StaplesSources { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BellRec;Trusted_Connection=True; Integrated Security=SSPI;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BellSource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BellSour__3214EC0714026113");

            entity.ToTable("BellSource");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.CustomerName).HasColumnType("text");
            entity.Property(e => e.Imei).HasColumnName("IMEI");
            entity.Property(e => e.Lob)
                .HasColumnType("text")
                .HasColumnName("LOB");
            entity.Property(e => e.MatchStatus).HasColumnType("text");
            entity.Property(e => e.RebateType).HasColumnType("text");
            entity.Property(e => e.Reconciled).HasColumnType("text");
            entity.Property(e => e.ReconciledBy).HasColumnType("text");
            entity.Property(e => e.ReconciledDate).HasColumnType("text");
            entity.Property(e => e.SubLob).HasColumnType("text");
            entity.Property(e => e.TransactionDate).HasColumnType("text");
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
            entity.HasKey(e => e.Id).HasName("PK__StaplesS__3214EC073AD709C4");

            entity.ToTable("StaplesSource");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Brand).HasColumnType("text");
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.CustomerName).HasColumnType("text");
            entity.Property(e => e.DeviceCo).HasColumnType("text");
            entity.Property(e => e.Imei).HasColumnName("IMEI");
            entity.Property(e => e.Lob)
                .HasColumnType("text")
                .HasColumnName("LOB");
            entity.Property(e => e.Location).HasColumnType("text");
            entity.Property(e => e.Msf).HasColumnName("MSF");
            entity.Property(e => e.Product).HasColumnType("text");
            entity.Property(e => e.RebateType).HasColumnType("text");
            entity.Property(e => e.Rec).HasColumnType("text");
            entity.Property(e => e.Reconciled).HasColumnType("text");
            entity.Property(e => e.ReconciledBy).HasColumnType("text");
            entity.Property(e => e.ReconciledDate).HasColumnType("text");
            entity.Property(e => e.SalesPerson).HasColumnType("text");
            entity.Property(e => e.SubLob).HasColumnType("text");
            entity.Property(e => e.TransactionDate).HasColumnType("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
