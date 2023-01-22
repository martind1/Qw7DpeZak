using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QwTest7.Models.KmpDb;

namespace QwTest7.Data;

public partial class KmpDbContext : DbContext
{
    public KmpDbContext(DbContextOptions<KmpDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AUSWAHLEN> AUSWAHLEN_Tbl { get; set; }

    public virtual DbSet<DATEIEN> DATEIEN_Tbl { get; set; }

    public virtual DbSet<FILTERABFRAGEN> FILTERABFRAGEN_Tbl { get; set; }

    public virtual DbSet<INITIALISIERUNGEN> INITIALISIERUNGEN_Tbl { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("QUSY");

        modelBuilder.Entity<AUSWAHLEN>(entity =>
        {
            entity.HasKey(e => e.ASWS_ID).HasName("PK_ASWS");

            entity.Property(e => e.ANZAHL_AENDERUNGEN).ValueGeneratedOnAdd();
            entity.Property(e => e.ERFASST_AM).ValueGeneratedOnAdd();
            entity.Property(e => e.ERFASST_DATENBANK).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_AM).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<DATEIEN>(entity =>
        {
            entity.HasKey(e => e.DATN_ID).HasName("PK_DATN");

            entity.Property(e => e.ANZAHL_AENDERUNGEN).ValueGeneratedOnAdd();
            entity.Property(e => e.ERFASST_AM).ValueGeneratedOnAdd();
            entity.Property(e => e.ERFASST_DATENBANK).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_AM).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_DATENBANK).ValueGeneratedOnAdd();
            entity.Property(e => e.INHALT).HasDefaultValueSql("EMPTY_BLOB()");
        });

        modelBuilder.Entity<FILTERABFRAGEN>(entity =>
        {
            entity.HasKey(e => e.FLTR_ID).HasName("PK_FLTR");

            entity.Property(e => e.ANWE).HasDefaultValueSql("'QUSY'                ");
            entity.Property(e => e.ANZAHL_AENDERUNGEN).ValueGeneratedOnAdd();
            entity.Property(e => e.ERFASST_AM).ValueGeneratedOnAdd();
            entity.Property(e => e.ERFASST_DATENBANK).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_AM).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_DATENBANK).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<INITIALISIERUNGEN>(entity =>
        {
            entity.HasKey(e => e.INIT_ID).HasName("PK_INIT");

            entity.Property(e => e.ANZAHL_AENDERUNGEN).ValueGeneratedOnAdd();
            entity.Property(e => e.ERFASST_AM).ValueGeneratedOnAdd();
            entity.Property(e => e.ERFASST_DATENBANK).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_AM).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_DATENBANK).ValueGeneratedOnAdd();
        });
        modelBuilder.HasSequence("ALLG_ID_S");
        modelBuilder.HasSequence("ANWE_ID_S");
        modelBuilder.HasSequence("ASWS_ID_S");
        modelBuilder.HasSequence("BATA_ID_S");
        modelBuilder.HasSequence("DATN_ID_S");
        modelBuilder.HasSequence("FELD_ID_S");
        modelBuilder.HasSequence("FLOG_ID_S");
        modelBuilder.HasSequence("FLTR_ID_S");
        modelBuilder.HasSequence("FMEL_ID_S");
        modelBuilder.HasSequence("FOFO_ID_S");
        modelBuilder.HasSequence("FORM_ID_S");
        modelBuilder.HasSequence("GRUP_ID_S");
        modelBuilder.HasSequence("INIT_ID_S");
        modelBuilder.HasSequence("KRGR_ID_S");
        modelBuilder.HasSequence("KRIT_ID_S");
        modelBuilder.HasSequence("OBJE_ID_S");
        modelBuilder.HasSequence("PRAE_ID_S");
        modelBuilder.HasSequence("PROG_ID_S");
        modelBuilder.HasSequence("RECH_ID_S");
        modelBuilder.HasSequence("REFO_ID_S");
        modelBuilder.HasSequence("REPO_ID_S");
        modelBuilder.HasSequence("SYSP_ID_S");
        modelBuilder.HasSequence("T_FELD_ID_S");
        modelBuilder.HasSequence("USER_ID_S");
        modelBuilder.HasSequence("USGR_ID_S");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
