using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QwTest7.Models.Qusy
{
    [Table("FILTERABFRAGEN", Schema = "QUSY")]
    public partial class Filterabfragen
    {
        public string ANWE { get; set; }

        [Required]
        public string FORM { get; set; }

        [Required]
        public string NAME { get; set; }

        public string FLTRLIST { get; set; }

        public string KEYFIELDS { get; set; }

        public string ISPUBLIC { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("FLTR_ID")]
        public int FLTRID { get; set; }

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

        public string COLUMNLIST { get; set; }

        public string FORMATLIST { get; set; }

    }
}