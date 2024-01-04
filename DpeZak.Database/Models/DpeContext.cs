﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DpeZak.Database.Models;

public partial class DpeContext : DbContext
{
    public DpeContext(DbContextOptions<DpeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asws> Asws { get; set; }

    public virtual DbSet<Bezi> Bezi { get; set; }

    public virtual DbSet<Fltr> Fltr { get; set; }

    public virtual DbSet<Kund> Kund { get; set; }

    public virtual DbSet<RDatn> RDatn { get; set; }

    public virtual DbSet<RInit> RInit { get; set; }

    public virtual DbSet<Vbez> Vbez { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asws>(entity =>
        {
            entity.HasKey(e => new { e.AswName, e.ItemPos });

            entity.ToTable("ASWS");

            entity.Property(e => e.AswName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ASW_NAME");
            entity.Property(e => e.ItemPos).HasColumnName("ITEM_POS");
            entity.Property(e => e.AnzahlAenderungen).HasColumnName("ANZAHL_AENDERUNGEN");
            entity.Property(e => e.Bemerkung)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("BEMERKUNG");
            entity.Property(e => e.ErfasstAm)
                .HasColumnType("datetime")
                .HasColumnName("ERFASST_AM");
            entity.Property(e => e.ErfasstVon)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ERFASST_VON");
            entity.Property(e => e.GeaendertAm)
                .HasColumnType("datetime")
                .HasColumnName("GEAENDERT_AM");
            entity.Property(e => e.GeaendertVon)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("GEAENDERT_VON");
            entity.Property(e => e.ItemDisplay)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ITEM_DISPLAY");
            entity.Property(e => e.ItemValue)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("ITEM_VALUE");
        });

        modelBuilder.Entity<Bezi>(entity =>
        {
            entity.HasKey(e => e.BeziNr);

            entity.ToTable("BEZI", tb =>
                {
                    tb.HasTrigger("TR_BEZI_CHANGELOG");
                    tb.HasTrigger("TR_BEZI_CHANGELOG_AFD");
                    tb.HasTrigger("TR_BEZI_CHANGELOG_AFI");
                });

            entity.HasIndex(e => e.BeziBez, "BEZI_BEZ").HasFillFactor(90);

            entity.Property(e => e.BeziNr)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("bezi_nr");
            entity.Property(e => e.AnzahlAenderungen).HasColumnName("anzahl_aenderungen");
            entity.Property(e => e.Bemerkung)
                .HasMaxLength(255)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("bemerkung");
            entity.Property(e => e.BeziBez)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("bezi_bez");
            entity.Property(e => e.ErfasstAm)
                .HasColumnType("datetime")
                .HasColumnName("erfasst_am");
            entity.Property(e => e.ErfasstVon)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("erfasst_von");
            entity.Property(e => e.GeaendertAm)
                .HasColumnType("datetime")
                .HasColumnName("geaendert_am");
            entity.Property(e => e.GeaendertVon)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("geaendert_von");
            entity.Property(e => e.VbezNr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("vbez_nr");
            entity.Property(e => e.ZoneNr).HasColumnName("zone_nr");

            entity.HasOne(d => d.VbezNrNavigation).WithMany(p => p.Bezi)
                .HasForeignKey(d => d.VbezNr)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_BEZI_VBEZ");
        });

        modelBuilder.Entity<Fltr>(entity =>
        {
            entity.ToTable("FLTR");

            entity.HasIndex(e => new { e.Form, e.Name }, "UK_FLTR").IsUnique();

            entity.Property(e => e.FltrId).HasColumnName("FLTR_ID");
            entity.Property(e => e.AnzahlAenderungen).HasColumnName("ANZAHL_AENDERUNGEN");
            entity.Property(e => e.Bemerkung)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("BEMERKUNG");
            entity.Property(e => e.Columnlist)
                .HasColumnType("text")
                .HasColumnName("COLUMNLIST");
            entity.Property(e => e.ErfasstAm)
                .HasColumnType("datetime")
                .HasColumnName("ERFASST_AM");
            entity.Property(e => e.ErfasstDatenbank)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ERFASST_DATENBANK");
            entity.Property(e => e.ErfasstVon)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ERFASST_VON");
            entity.Property(e => e.Fltrlist)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("FLTRLIST");
            entity.Property(e => e.Form)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FORM");
            entity.Property(e => e.GeaendertAm)
                .HasColumnType("datetime")
                .HasColumnName("GEAENDERT_AM");
            entity.Property(e => e.GeaendertDatenbank)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("GEAENDERT_DATENBANK");
            entity.Property(e => e.GeaendertVon)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("GEAENDERT_VON");
            entity.Property(e => e.Ispublic)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("ISPUBLIC");
            entity.Property(e => e.Keyfields)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("KEYFIELDS");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Kund>(entity =>
        {
            entity.HasKey(e => e.KundNr);

            entity.ToTable("KUND", tb =>
                {
                    tb.HasTrigger("TR_KUND_CHANGELOG");
                    tb.HasTrigger("TR_KUND_CHANGELOG_AFD");
                    tb.HasTrigger("TR_KUND_CHANGELOG_AFI");
                });

            entity.HasIndex(e => e.KundBefnr, "KUND_BEFNR");

            entity.HasIndex(e => e.KundErznr, "KUND_ERZNR");

            entity.HasIndex(e => e.KundNa1, "KUND_NA1").HasFillFactor(90);

            entity.HasIndex(e => e.KundNa2, "KUND_NA2").HasFillFactor(90);

            entity.HasIndex(e => new { e.KundOrt, e.KundStr, e.KundHnr }, "KUND_ORT").HasFillFactor(90);

            entity.HasIndex(e => e.KundPlz, "KUND_PLZ").HasFillFactor(90);

            entity.HasIndex(e => e.KundStr, "KUND_STR").HasFillFactor(90);

            entity.Property(e => e.KundNr)
                .ValueGeneratedNever()
                .HasColumnName("kund_nr");
            entity.Property(e => e.AnzahlAenderungen).HasColumnName("anzahl_aenderungen");
            entity.Property(e => e.Bankkonto)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("BANKKONTO");
            entity.Property(e => e.Bankleitzahl)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("BANKLEITZAHL");
            entity.Property(e => e.Bankname)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("BANKNAME");
            entity.Property(e => e.Bankort)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("BANKORT");
            entity.Property(e => e.Barerlaubt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("BARERLAUBT");
            entity.Property(e => e.Bemerkung)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("bemerkung");
            entity.Property(e => e.BeziNr)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("bezi_nr");
            entity.Property(e => e.Bic)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("BIC");
            entity.Property(e => e.Bmhk)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("BMHK");
            entity.Property(e => e.Bonitaet)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("N")
                .HasColumnName("BONITAET");
            entity.Property(e => e.Buergschaft)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("BUERGSCHAFT");
            entity.Property(e => e.BuergschaftBetrag)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("BUERGSCHAFT_BETRAG");
            entity.Property(e => e.Dtllzah)
                .HasColumnType("datetime")
                .HasColumnName("DTLLZAH");
            entity.Property(e => e.EanvAdre)
                .HasColumnType("text")
                .HasColumnName("EANV_ADRE");
            entity.Property(e => e.EanvDiffadre)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("EANV_DIFFADRE");
            entity.Property(e => e.EanvFirma)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("EANV_FIRMA");
            entity.Property(e => e.EanvZertifikat)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("N")
                .HasColumnName("EANV_ZERTIFIKAT");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.EntgeltDru)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("entgelt_dru");
            entity.Property(e => e.ErfasstAm)
                .HasColumnType("datetime")
                .HasColumnName("erfasst_am");
            entity.Property(e => e.ErfasstVon)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("erfasst_von");
            entity.Property(e => e.FibuLfrtNr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FIBU_LFRT_NR");
            entity.Property(e => e.GeaendertAm)
                .HasColumnType("datetime")
                .HasColumnName("geaendert_am");
            entity.Property(e => e.GeaendertVon)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("geaendert_von");
            entity.Property(e => e.Iban)
                .HasMaxLength(34)
                .IsUnicode(false)
                .HasColumnName("IBAN");
            entity.Property(e => e.KugrName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("kugr_name");
            entity.Property(e => e.KugrNr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("KUGR_NR");
            entity.Property(e => e.KundAnl)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("kund_anl");
            entity.Property(e => e.KundAnrede)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("kund_anrede");
            entity.Property(e => e.KundAnspr)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("kund_anspr");
            entity.Property(e => e.KundAnzlfs).HasColumnName("kund_anzlfs");
            entity.Property(e => e.KundAnzre).HasColumnName("kund_anzre");
            entity.Property(e => e.KundBefnr)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("kund_befnr");
            entity.Property(e => e.KundBeziIntern)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("KUND_BEZI_INTERN");
            entity.Property(e => e.KundDisk)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("kund_disk");
            entity.Property(e => e.KundDivers)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("kund_divers");
            entity.Property(e => e.KundEdv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("kund_edv");
            entity.Property(e => e.KundEntnr)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("kund_entnr");
            entity.Property(e => e.KundEnts)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("kund_ents");
            entity.Property(e => e.KundErz)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("kund_erz");
            entity.Property(e => e.KundErznr)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("kund_erznr");
            entity.Property(e => e.KundFax)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("kund_fax");
            entity.Property(e => e.KundFkgruEn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("N")
                .HasColumnName("KUND_FKGRU_EN");
            entity.Property(e => e.KundFkgruSr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("N")
                .HasColumnName("KUND_FKGRU_SR");
            entity.Property(e => e.KundFksor).HasColumnName("kund_fksor");
            entity.Property(e => e.KundFktur).HasColumnName("kund_fktur");
            entity.Property(e => e.KundFktyp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("kund_fktyp");
            entity.Property(e => e.KundFrdtm)
                .HasColumnType("datetime")
                .HasColumnName("kund_frdtm");
            entity.Property(e => e.KundFrmen).HasColumnName("kund_frmen");
            entity.Property(e => e.KundGes1)
                .HasMaxLength(28)
                .IsUnicode(false)
                .HasColumnName("kund_ges1");
            entity.Property(e => e.KundGes2)
                .HasMaxLength(28)
                .IsUnicode(false)
                .HasColumnName("kund_ges2");
            entity.Property(e => e.KundGugruSr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("N")
                .HasColumnName("KUND_GUGRU_SR");
            entity.Property(e => e.KundHnr)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("kund_hnr");
            entity.Property(e => e.KundLand)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("kund_land");
            entity.Property(e => e.KundLcode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("kund_lcode");
            entity.Property(e => e.KundNa1)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("kund_na1");
            entity.Property(e => e.KundNa2)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("kund_na2");
            entity.Property(e => e.KundNa3)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("KUND_NA3");
            entity.Property(e => e.KundNa4)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("KUND_NA4");
            entity.Property(e => e.KundOrt)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("kund_ort");
            entity.Property(e => e.KundOrt2)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("KUND_ORT2");
            entity.Property(e => e.KundPlz)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("kund_plz");
            entity.Property(e => e.KundStr)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("kund_str");
            entity.Property(e => e.KundStr2)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("KUND_STR2");
            entity.Property(e => e.KundTel)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("kund_tel");
            entity.Property(e => e.KundZah)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("kund_zah");
            entity.Property(e => e.KundZahNr).HasColumnName("KUND_ZAH_NR");
            entity.Property(e => e.KundZatyp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("kund_zatyp");
            entity.Property(e => e.Lastschrift)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("LASTSCHRIFT");
            entity.Property(e => e.LastschriftBmhk)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("0")
                .HasColumnName("LASTSCHRIFT_BMHK");
            entity.Property(e => e.OpMaxbetrag).HasColumnName("OP_MAXBETRAG");
            entity.Property(e => e.Rechtext)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RECHTEXT");
            entity.Property(e => e.SammelKto)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("SAMMEL_KTO");
            entity.Property(e => e.Ustid)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("USTID");
            entity.Property(e => e.Www)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("WWW");

            entity.HasOne(d => d.BeziNrNavigation).WithMany(p => p.Kund)
                .HasForeignKey(d => d.BeziNr)
                .HasConstraintName("FK_KUND_BEZI");
        });

        modelBuilder.Entity<RDatn>(entity =>
        {
            entity.HasKey(e => e.DatnId).HasName("PK_DATN");

            entity.ToTable("R_DATN", tb =>
                {
                    tb.HasTrigger("DATN_ARI");
                    tb.HasTrigger("DATN_ARU");
                });

            entity.HasIndex(e => new { e.Ordner, e.Filename }, "UK_DATN").IsUnique();

            entity.Property(e => e.DatnId)
                .HasDefaultValueSql("([dbo].[NEW_ID_READONLY]('DATN_ID'))")
                .HasColumnName("DATN_ID");
            entity.Property(e => e.AnzahlAenderungen).HasColumnName("ANZAHL_AENDERUNGEN");
            entity.Property(e => e.Bemerkung)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("BEMERKUNG");
            entity.Property(e => e.ErfasstAm)
                .HasColumnType("datetime")
                .HasColumnName("ERFASST_AM");
            entity.Property(e => e.ErfasstDatenbank)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ERFASST_DATENBANK");
            entity.Property(e => e.ErfasstVon)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ERFASST_VON");
            entity.Property(e => e.Filename)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FILENAME");
            entity.Property(e => e.Filetime)
                .HasColumnType("datetime")
                .HasColumnName("FILETIME");
            entity.Property(e => e.GeaendertAm)
                .HasColumnType("datetime")
                .HasColumnName("GEAENDERT_AM");
            entity.Property(e => e.GeaendertDatenbank)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("GEAENDERT_DATENBANK");
            entity.Property(e => e.GeaendertVon)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("GEAENDERT_VON");
            entity.Property(e => e.Inhalt)
                .HasColumnType("image")
                .HasColumnName("INHALT");
            entity.Property(e => e.Ordner)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ORDNER");
        });

        modelBuilder.Entity<RInit>(entity =>
        {
            entity.HasKey(e => e.InitId).HasName("PK_INIT");

            entity.ToTable("R_INIT", tb =>
                {
                    tb.HasTrigger("INIT_ARI");
                    tb.HasTrigger("INIT_ARU");
                    tb.HasTrigger("INIT_IOI");
                });

            entity.HasIndex(e => new { e.Anwendung, e.Typ, e.Name, e.Section, e.Param }, "UK_INIT").IsUnique();

            entity.Property(e => e.InitId)
                .HasDefaultValueSql("([dbo].[NEW_ID_READONLY]('INIT_ID'))")
                .HasColumnName("INIT_ID");
            entity.Property(e => e.Anwendung)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ANWENDUNG");
            entity.Property(e => e.AnzahlAenderungen).HasColumnName("ANZAHL_AENDERUNGEN");
            entity.Property(e => e.Bemerkung)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("BEMERKUNG");
            entity.Property(e => e.ErfasstAm)
                .HasColumnType("datetime")
                .HasColumnName("ERFASST_AM");
            entity.Property(e => e.ErfasstDatenbank)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ERFASST_DATENBANK");
            entity.Property(e => e.ErfasstVon)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ERFASST_VON");
            entity.Property(e => e.GeaendertAm)
                .HasColumnType("datetime")
                .HasColumnName("GEAENDERT_AM");
            entity.Property(e => e.GeaendertDatenbank)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("GEAENDERT_DATENBANK");
            entity.Property(e => e.GeaendertVon)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("GEAENDERT_VON");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Param)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PARAM");
            entity.Property(e => e.Section)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SECTION");
            entity.Property(e => e.Typ)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("TYP");
            entity.Property(e => e.Wert)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("WERT");
        });

        modelBuilder.Entity<Vbez>(entity =>
        {
            entity.HasKey(e => e.VbezNr);

            entity.ToTable("VBEZ", tb => tb.HasTrigger("TR_VBEZ_CHANGELOG"));

            entity.Property(e => e.VbezNr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("vbez_nr");
            entity.Property(e => e.AnzahlAenderungen).HasColumnName("anzahl_aenderungen");
            entity.Property(e => e.Bemerkung)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("bemerkung");
            entity.Property(e => e.ErfasstAm)
                .HasColumnType("datetime")
                .HasColumnName("erfasst_am");
            entity.Property(e => e.ErfasstVon)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("erfasst_von");
            entity.Property(e => e.GeaendertAm)
                .HasColumnType("datetime")
                .HasColumnName("geaendert_am");
            entity.Property(e => e.GeaendertVon)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("geaendert_von");
            entity.Property(e => e.VbezBez)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("vbez_bez");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}