using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QwTest7.Models.Quva
{
    [Table("SPEDITIONEN", Schema = "QUVA")]
    public partial class Speditionen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SPED_ID")]
        public int SPEDID { get; set; }

        [Column("SPED_NAME")]
        [Required]
        public string SPEDNAME { get; set; }

        [Column("WERK_NR")]
        [Required]
        public string WERKNR { get; set; }

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

        [Column("NAME_LANG")]
        public string NAMELANG { get; set; }

        [Column("KREDITOR_NR")]
        public string KREDITORNR { get; set; }

        public string PALETTENKONTO { get; set; }

        [Column("PAL_INV_DTM")]
        public DateTime? PALINVDTM { get; set; }

        [Column("PAL_INV_BST_E")]
        public int? PALINVBSTE { get; set; }

        [Column("PAL_INV_BST_D")]
        public int? PALINVBSTD { get; set; }

        public IEnumerable<Fahrzeuge> Fahrzeuges { get; set; }

        public IEnumerable<Karten> Kartens { get; set; }

    }
}