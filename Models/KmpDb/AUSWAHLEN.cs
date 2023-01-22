using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
using Microsoft.EntityFrameworkCore;

namespace QwTest7.Models.KmpDb;

[Table("AUSWAHLEN")]
[Index("ASW_NAME", "ITEM_POS", Name = "UK1_ASWS", IsUnique = true)]
public partial class AUSWAHLEN
{
    [Key]
    [Precision(9)]
    public int ASWS_ID { get; set; }

    [Required]
    [StringLength(30)]
    [Unicode(false)]
    public string ASW_NAME { get; set; }

    [Column(TypeName = "NUMBER(38)")]
    public decimal ITEM_POS { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string ITEM_VALUE { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string ITEM_DISPLAY { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string ERFASST_VON { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? ERFASST_AM { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string ERFASST_DATENBANK { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string GEAENDERT_VON { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? GEAENDERT_AM { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string GEAENDERT_DATENBANK { get; set; }

    [Precision(9)]
    public int? ANZAHL_AENDERUNGEN { get; set; }

    [StringLength(2000)]
    [Unicode(false)]
    public string BEMERKUNG { get; set; }
}
