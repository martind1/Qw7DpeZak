using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QwTest7.Models.Blacki;

[Microsoft.EntityFrameworkCore.Index("WERK_NR", "TRANSPORTMITTEL", "SPEDITION", Name = "UK_FRZG", IsUnique = true)]
public partial class FAHRZEUGE
{
    [Key]
    [Precision(9)]
    public int FRZG_ID { get; set; }

    [Required]
    [StringLength(30)]
    [Unicode(false)]
    public string TRANSPORTMITTEL { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string WERK_NR { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string SPEDITION { get; set; }

    [Column(TypeName = "NUMBER(9,3)")]
    public decimal? TARA_GEWICHT { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? TARA_DATUM { get; set; }

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

    [StringLength(30)]
    [Unicode(false)]
    public string FAHRZEUGTYP { get; set; }

    [Precision(5)]
    public short? TARA_FREI_ANZAHL { get; set; }

    [Column(TypeName = "NUMBER(9,3)")]
    public decimal? MAX_BRUTTO { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string FLAGS { get; set; }

    [Precision(9)]
    public int? SPED_ID { get; set; }

    [InverseProperty("FRZG")]
    public virtual ICollection<KARTEN> KARTEN { get; } = new List<KARTEN>();

    [ForeignKey("SPED_ID")]
    [InverseProperty("FAHRZEUGE")]
    public virtual SPEDITIONEN SPED { get; set; }
}
