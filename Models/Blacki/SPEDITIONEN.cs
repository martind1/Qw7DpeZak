using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QwTest7.Models.Blacki;

[Index("WERK_NR", "SPED_NAME", Name = "UK_SPED", IsUnique = true)]
public partial class SPEDITIONEN
{
    [Key]
    [Precision(9)]
    public int SPED_ID { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string SPED_NAME { get; set; }

    [Required]
    [StringLength(4)]
    [Unicode(false)]
    public string WERK_NR { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string REPLIKATION { get; set; }

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

    [StringLength(500)]
    [Unicode(false)]
    public string NAME_LANG { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string KREDITOR_NR { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string PALETTENKONTO { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? PAL_INV_DTM { get; set; }

    [Precision(6)]
    public int? PAL_INV_BST_E { get; set; }

    [Precision(6)]
    public int? PAL_INV_BST_D { get; set; }

    [InverseProperty("SPED")]
    public virtual ICollection<FAHRZEUGE> FAHRZEUGE { get; } = new List<FAHRZEUGE>();

    [InverseProperty("SPED")]
    public virtual ICollection<KARTEN> KARTEN { get; } = new List<KARTEN>();
}
