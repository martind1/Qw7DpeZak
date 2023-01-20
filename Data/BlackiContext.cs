using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QwTest7.Models.Blacki;

namespace QwTest7.Data;

public partial class BlackiContext : DbContext
{
    public BlackiContext(DbContextOptions<BlackiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FAHRZEUGE> FAHRZEUGE_Tbl { get; set; }

    public virtual DbSet<KARTEN> KARTEN_Tbl { get; set; }

    public virtual DbSet<SPEDITIONEN> SPEDITIONEN_Tbl { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("QUVA");

        modelBuilder.Entity<FAHRZEUGE>(entity =>
        {
            entity.HasKey(e => e.FRZG_ID).HasName("PK_FRZG");

            entity.Property(e => e.ANZAHL_AENDERUNGEN).ValueGeneratedOnAdd();
            entity.Property(e => e.BEMERKUNG).ValueGeneratedOnAdd();
            entity.Property(e => e.ERFASST_AM).ValueGeneratedOnAdd();
            entity.Property(e => e.ERFASST_DATENBANK).ValueGeneratedOnAdd();
            entity.Property(e => e.FAHRZEUGTYP).ValueGeneratedOnAdd();
            entity.Property(e => e.FLAGS).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_AM).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_DATENBANK).ValueGeneratedOnAdd();
            entity.Property(e => e.MAX_BRUTTO).ValueGeneratedOnAdd();
            entity.Property(e => e.REPLIKATION).ValueGeneratedOnAdd();
            entity.Property(e => e.SPEDITION).ValueGeneratedOnAdd();
            entity.Property(e => e.SPED_ID).ValueGeneratedOnAdd();
            entity.Property(e => e.TARA_DATUM).ValueGeneratedOnAdd();
            entity.Property(e => e.TARA_FREI_ANZAHL).ValueGeneratedOnAdd();
            entity.Property(e => e.TARA_GEWICHT).ValueGeneratedOnAdd();
            entity.Property(e => e.TRANSPORTMITTEL).ValueGeneratedOnAdd();
            entity.Property(e => e.WERK_NR).ValueGeneratedOnAdd();

            entity.HasOne(d => d.SPED).WithMany(p => p.FAHRZEUGE).HasConstraintName("FK_FRZG_SPED");
        });

        modelBuilder.Entity<KARTEN>(entity =>
        {
            entity.HasKey(e => e.KART_ID).HasName("PK_KART");

            entity.Property(e => e.ANZAHL_AENDERUNGEN).ValueGeneratedOnAdd();
            entity.Property(e => e.AUFBAU).ValueGeneratedOnAdd();
            entity.Property(e => e.AUFK_NR).ValueGeneratedOnAdd();
            entity.Property(e => e.BEMERKUNG).ValueGeneratedOnAdd();
            entity.Property(e => e.EINGANG_KNZ)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("'N'");
            entity.Property(e => e.ERFASST_AM).ValueGeneratedOnAdd();
            entity.Property(e => e.ERFASST_DATENBANK).ValueGeneratedOnAdd();
            entity.Property(e => e.FREMDWAEGUNG)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("'N'");
            entity.Property(e => e.FRZG_ID).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_AM).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_DATENBANK).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_VON).ValueGeneratedOnAdd();
            entity.Property(e => e.KART_NR).ValueGeneratedOnAdd();
            entity.Property(e => e.KOMBI_KNZ).HasDefaultValueSql("'N'\n");
            entity.Property(e => e.KUND_KNZ)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("'J'");
            entity.Property(e => e.KUNW_NR).ValueGeneratedOnAdd();
            entity.Property(e => e.MARA_NR).ValueGeneratedOnAdd();
            entity.Property(e => e.MAX_BRUTTO).ValueGeneratedOnAdd();
            entity.Property(e => e.ORI_KART_NR).ValueGeneratedOnAdd();
            entity.Property(e => e.PEKR_BEZ).ValueGeneratedOnAdd();
            entity.Property(e => e.PEKR_ID).ValueGeneratedOnAdd();
            entity.Property(e => e.PEKR_NR).ValueGeneratedOnAdd();
            entity.Property(e => e.PROBENPFLICHT)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("'J'");
            entity.Property(e => e.REPLIKATION).ValueGeneratedOnAdd();
            entity.Property(e => e.SILO_NR).ValueGeneratedOnAdd();
            entity.Property(e => e.SOLLMENGE).ValueGeneratedOnAdd();
            entity.Property(e => e.SORTE_KNZ)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("'N'");
            entity.Property(e => e.SPEDITION).ValueGeneratedOnAdd();
            entity.Property(e => e.SPED_ID).ValueGeneratedOnAdd();
            entity.Property(e => e.SPENDER_KNZ)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("'N'");
            entity.Property(e => e.SPERR_KNZ)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("'N'");
            entity.Property(e => e.TARA_DATUM).ValueGeneratedOnAdd();
            entity.Property(e => e.TARA_GEWICHT).ValueGeneratedOnAdd();
            entity.Property(e => e.TEILMENGE1).ValueGeneratedOnAdd();
            entity.Property(e => e.TEILMENGE2).ValueGeneratedOnAdd();
            entity.Property(e => e.TEILMENGE3).ValueGeneratedOnAdd();
            entity.Property(e => e.TEILMENGE4).ValueGeneratedOnAdd();
            entity.Property(e => e.TEILMENGE5).ValueGeneratedOnAdd();
            entity.Property(e => e.TRANSPORTMITTEL).ValueGeneratedOnAdd();
            entity.Property(e => e.TROCKEN_FEUCHT).ValueGeneratedOnAdd();
            entity.Property(e => e.WERK_NR).ValueGeneratedOnAdd();

            entity.HasOne(d => d.FRZG).WithMany(p => p.KARTEN).HasConstraintName("FK_KART_FRZG");

            entity.HasOne(d => d.SPED).WithMany(p => p.KARTEN).HasConstraintName("FK_KART_SPED");
        });

        modelBuilder.Entity<SPEDITIONEN>(entity =>
        {
            entity.HasKey(e => e.SPED_ID).HasName("PK_SPED");

            entity.Property(e => e.ANZAHL_AENDERUNGEN).ValueGeneratedOnAdd();
            entity.Property(e => e.BEMERKUNG).ValueGeneratedOnAdd();
            entity.Property(e => e.ERFASST_AM).ValueGeneratedOnAdd();
            entity.Property(e => e.ERFASST_DATENBANK).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_AM).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_DATENBANK).ValueGeneratedOnAdd();
            entity.Property(e => e.KREDITOR_NR).ValueGeneratedOnAdd();
            entity.Property(e => e.NAME_LANG).ValueGeneratedOnAdd();
            entity.Property(e => e.PALETTENKONTO)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("'N'\n");
            entity.Property(e => e.PAL_INV_BST_D).ValueGeneratedOnAdd();
            entity.Property(e => e.PAL_INV_BST_E).ValueGeneratedOnAdd();
            entity.Property(e => e.PAL_INV_DTM).ValueGeneratedOnAdd();
            entity.Property(e => e.REPLIKATION).ValueGeneratedOnAdd();
            entity.Property(e => e.SPED_NAME).ValueGeneratedOnAdd();
            entity.Property(e => e.WERK_NR).ValueGeneratedOnAdd();
        });
        modelBuilder.HasSequence("ADRE_ID_S");
        modelBuilder.HasSequence("APKL_ID_S");
        modelBuilder.HasSequence("AUFK_ID_S");
        modelBuilder.HasSequence("AUFP_ID_S");
        modelBuilder.HasSequence("AURP_ID_S");
        modelBuilder.HasSequence("AUSL_ID_S");
        modelBuilder.HasSequence("BAUS_ID_S");
        modelBuilder.HasSequence("BCOD_ID_S");
        modelBuilder.HasSequence("BDAM_ID_S");
        modelBuilder.HasSequence("BDAT_ID_S");
        modelBuilder.HasSequence("BEIN_ID_S");
        modelBuilder.HasSequence("BELA_ID_S");
        modelBuilder.HasSequence("BELF_ID_S");
        modelBuilder.HasSequence("BELS_ID_S");
        modelBuilder.HasSequence("BILD_ID_S");
        modelBuilder.HasSequence("BITM_ID_S");
        modelBuilder.HasSequence("CHRG_ID_S");
        modelBuilder.HasSequence("DISP_ID_S");
        modelBuilder.HasSequence("DIVO_ID_S");
        modelBuilder.HasSequence("DSTA_ID_S");
        modelBuilder.HasSequence("EINT_ID_S");
        modelBuilder.HasSequence("EINV_ID_S");
        modelBuilder.HasSequence("EMAI_ID_S");
        modelBuilder.HasSequence("EMVE_ID_S");
        modelBuilder.HasSequence("ERRM_ID_S");
        modelBuilder.HasSequence("FLTR_ID_S");
        modelBuilder.HasSequence("FRZG_ID_S");
        modelBuilder.HasSequence("FRZT_ID_S");
        modelBuilder.HasSequence("GRSO_ID_S");
        modelBuilder.HasSequence("KART_ID_S");
        modelBuilder.HasSequence("LAER_ID_S");
        modelBuilder.HasSequence("LAND_ID_S");
        modelBuilder.HasSequence("LAQP_ID_S");
        modelBuilder.HasSequence("LASCH_NR_S");
        modelBuilder.HasSequence("LFSK_ID_S");
        modelBuilder.HasSequence("LFSP_ID_S");
        modelBuilder.HasSequence("LFST_ID_S");
        modelBuilder.HasSequence("LVLB_ID_S");
        modelBuilder.HasSequence("LVLBMA_ID_S");
        modelBuilder.HasSequence("LVLBZU_ID_S");
        modelBuilder.HasSequence("LVLG_ID_S");
        modelBuilder.HasSequence("LVLO_ID_S");
        modelBuilder.HasSequence("LVMABE_ID_S");
        modelBuilder.HasSequence("LVMAZU_ID_S");
        modelBuilder.HasSequence("MARA_ID_S");
        modelBuilder.HasSequence("MARF_ID_S");
        modelBuilder.HasSequence("PBER_ID_S");
        modelBuilder.HasSequence("PEKR_ID_S");
        modelBuilder.HasSequence("PRDG_ID_S");
        modelBuilder.HasSequence("PROT_ID_S");
        modelBuilder.HasSequence("SAUF_ID_S");
        modelBuilder.HasSequence("SILO_ID_S");
        modelBuilder.HasSequence("SIST_ID_S");
        modelBuilder.HasSequence("SIZU_ID_S");
        modelBuilder.HasSequence("SONF_ID_S");
        modelBuilder.HasSequence("SPED_ID_S");
        modelBuilder.HasSequence("SPGD_ID_S");
        modelBuilder.HasSequence("SPPA_ID_S");
        modelBuilder.HasSequence("SQ_AspNetRoleClaims");
        modelBuilder.HasSequence("SQ_AspNetUserClaims");
        modelBuilder.HasSequence("STME_ID_S");
        modelBuilder.HasSequence("SYPA_ID_S");
        modelBuilder.HasSequence("TEMP_LFSK_NR_S");
        modelBuilder.HasSequence("TOUR_NR_S");
        modelBuilder.HasSequence("TRAN_ID_S");
        modelBuilder.HasSequence("USSE_ID_S");
        modelBuilder.HasSequence("VART_ID_S");
        modelBuilder.HasSequence("VEBU_ID_S");
        modelBuilder.HasSequence("VERS_ID_S");
        modelBuilder.HasSequence("VKOR_ID_S");
        modelBuilder.HasSequence("WAGI_ID_S");
        modelBuilder.HasSequence("WATT_ID_S");
        modelBuilder.HasSequence("WERK_ID_S");
        modelBuilder.HasSequence("WERN_ID_S");
        modelBuilder.HasSequence("WERP_ID_S");
        modelBuilder.HasSequence("ZCHP_ID_S");
        modelBuilder.HasSequence("ZERT_ID_S");
        modelBuilder.HasSequence("ZGRL_ID_S");
        modelBuilder.HasSequence("ZGRP_ID_S");
        modelBuilder.HasSequence("ZLPGR_ID_S");
        modelBuilder.HasSequence("ZSPKU_ID_S");
        modelBuilder.HasSequence("ZUST_ID_S");
        modelBuilder.HasSequence("ZUTY_ID_S");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
