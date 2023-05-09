using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bell.Reconciliation.Web.Server.Data;

public partial class BellRecContext : DbContext
{
    public BellRecContext()
    {
    }

    public BellRecContext(DbContextOptions<BellRecContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Data.BellSource> BellSources { get; set; }

    public virtual DbSet<Data.StaplesSource> StaplesSources { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=data\\BellRec.sqlite; providerName=");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Data.BellSource>(entity =>
        {
            entity.ToTable("BellSource");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Imei).HasColumnName("IMEI");
            entity.Property(e => e.Lob).HasColumnName("LOB");
        });

        modelBuilder.Entity<Data.StaplesSource>(entity =>
        {
            entity.ToTable("StaplesSource");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Imei).HasColumnName("IMEI");
            entity.Property(e => e.Msf).HasColumnName("MSF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
