using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QwTest7.Models.Quva
{
    [Table("FAHRZEUGE", Schema = "QUVA")]
    public partial class Fahrzeuge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("FRZG_ID")]
        public int FRZGID { get; set; }

        [Required]
        public string TRANSPORTMITTEL { get; set; }

        [Column("WERK_NR")]
        public string WERKNR { get; set; }

        public string SPEDITION { get; set; }

        [Column("TARA_GEWICHT")]
        public decimal? TARAGEWICHT { get; set; }

        [Column("TARA_DATUM")]
        public DateTime? TARADATUM { get; set; }

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

        public string FAHRZEUGTYP { get; set; }

        [Column("TARA_FREI_ANZAHL")]
        public short? TARAFREIANZAHL { get; set; }

        [Column("MAX_BRUTTO")]
        public decimal? MAXBRUTTO { get; set; }

        public string FLAGS { get; set; }

        [Column("SPED_ID")]
        public int? SPEDID { get; set; }

        public Speditionen Speditionen { get; set; }

        public IEnumerable<Karten> Kartens { get; set; }

    }
}