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

    public virtual DbSet<FILTERABFRAGEN> FILTERABFRAGEN_Tbl { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("QUVA");

        modelBuilder.Entity<FILTERABFRAGEN>(entity =>
        {
            entity.HasKey(e => e.FLTR_ID).HasName("PK_FLTR");

            entity.Property(e => e.ANZAHL_AENDERUNGEN).ValueGeneratedOnAdd();
            entity.Property(e => e.ERFASST_AM).ValueGeneratedOnAdd();
            entity.Property(e => e.ERFASST_DATENBANK).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_AM).ValueGeneratedOnAdd();
            entity.Property(e => e.GEAENDERT_DATENBANK).ValueGeneratedOnAdd();
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
