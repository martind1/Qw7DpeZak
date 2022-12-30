using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QwTest7.Models.Quva
{
    [Table("KARTEN", Schema = "QUVA")]
    public partial class Karten
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("KART_ID")]
        public int KARTID { get; set; }

        [Column("KART_NR")]
        [Required]
        public string KARTNR { get; set; }

        [Column("KUNW_NR")]
        public string KUNWNR { get; set; }

        public string REPLIKATION { get; set; }

        [Column("ERFASST_VON")]
        public string ERFASSTVON { get; set; }

        [Column("ERFASST_AM")]
        public DateTime? ERFASSTAM { get; set; }

        [Column("ERFASST_DATENBANK")]
        public string ERFASSTDATENBANK { get; set; }

        [Column("GEAENDERT_VON")]
        public string GEAENDERTVON { get; set; }

        [Column("GEAENDERT_AM")]
        public DateTime? GEAENDERTAM { get; set; }

        [Column("GEAENDERT_DATENBANK")]
        public string GEAENDERTDATENBANK { get; set; }

        [Column("ANZAHL_AENDERUNGEN")]
        public int? ANZAHLAENDERUNGEN { get; set; }

        public string BEMERKUNG { get; set; }

        [Column("WERK_NR")]
        public string WERKNR { get; set; }

        [Column("MARA_NR")]
        public string MARANR { get; set; }

        public string TRANSPORTMITTEL { get; set; }

        public string SPEDITION { get; set; }

        [Column("AUFK_NR")]
        public string AUFKNR { get; set; }

        public decimal? SOLLMENGE { get; set; }

        [Column("PEKR_ID")]
        public int? PEKRID { get; set; }

        [Column("PEKR_NR")]
        public string PEKRNR { get; set; }

        [Column("PEKR_BEZ")]
        public string PEKRBEZ { get; set; }

        [Column("KUND_KNZ")]
        public string KUNDKNZ { get; set; }

        [Column("SPERR_KNZ")]
        public string SPERRKNZ { get; set; }

        [Column("SPENDER_KNZ")]
        public string SPENDERKNZ { get; set; }

        [Column("SILO_NR")]
        public byte? SILONR { get; set; }

        [Column("SORTE_KNZ")]
        public string SORTEKNZ { get; set; }

        public string FREMDWAEGUNG { get; set; }

        [Column("TARA_GEWICHT")]
        public decimal? TARAGEWICHT { get; set; }

        [Column("TARA_DATUM")]
        public DateTime? TARADATUM { get; set; }

        [Column("MAX_BRUTTO")]
        public decimal? MAXBRUTTO { get; set; }

        [Column("TROCKEN_FEUCHT")]
        public string TROCKENFEUCHT { get; set; }

        public string PROBENPFLICHT { get; set; }

        public decimal? TEILMENGE1 { get; set; }

        public decimal? TEILMENGE2 { get; set; }

        public decimal? TEILMENGE3 { get; set; }

        public decimal? TEILMENGE4 { get; set; }

        public decimal? TEILMENGE5 { get; set; }

        [Column("ORI_KART_NR")]
        public string ORIKARTNR { get; set; }

        [Column("EINGANG_KNZ")]
        public string EINGANGKNZ { get; set; }

        public string AUFBAU { get; set; }

        [Column("TARA_TAGE")]
        public short? TARATAGE { get; set; }

        [Column("KART_NR_2")]
        public string KARTNR2 { get; set; }

        [Column("KOMBI_KNZ")]
        public string KOMBIKNZ { get; set; }

        [Column("SPED_ID")]
        public int? SPEDID { get; set; }

        [Column("FRZG_ID")]
        public int? FRZGID { get; set; }

        public Fahrzeuge Fahrzeuge { get; set; }

        public Speditionen Speditionen { get; set; }

    }
}