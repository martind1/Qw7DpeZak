using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QwTest7.Models.Blacki;

[Index("KART_NR_2", "WERK_NR", Name = "I_KART_2")]
[Index("KART_NR", "WERK_NR", Name = "UK1_KART", IsUnique = true)]
public partial class KARTEN
{
    [Key]
    [Precision(9)]
    public int KART_ID { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string KART_NR { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string KUNW_NR { get; set; }

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

    [StringLength(4)]
    [Unicode(false)]
    public string WERK_NR { get; set; }

    [StringLength(18)]
    [Unicode(false)]
    public string MARA_NR { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string TRANSPORTMITTEL { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string SPEDITION { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string AUFK_NR { get; set; }

    [Column(TypeName = "NUMBER(9,3)")]
    public decimal? SOLLMENGE { get; set; }

    [Precision(9)]
    public int? PEKR_ID { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string PEKR_NR { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string PEKR_BEZ { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string KUND_KNZ { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string SPERR_KNZ { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string SPENDER_KNZ { get; set; }

    [Precision(3)]
    public byte? SILO_NR { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string SORTE_KNZ { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string FREMDWAEGUNG { get; set; }

    [Column(TypeName = "NUMBER(9,3)")]
    public decimal? TARA_GEWICHT { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? TARA_DATUM { get; set; }

    [Column(TypeName = "NUMBER(9,3)")]
    public decimal? MAX_BRUTTO { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string TROCKEN_FEUCHT { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string PROBENPFLICHT { get; set; }

    [Column(TypeName = "NUMBER(9,3)")]
    public decimal? TEILMENGE1 { get; set; }

    [Column(TypeName = "NUMBER(9,3)")]
    public decimal? TEILMENGE2 { get; set; }

    [Column(TypeName = "NUMBER(9,3)")]
    public decimal? TEILMENGE3 { get; set; }

    [Column(TypeName = "NUMBER(9,3)")]
    public decimal? TEILMENGE4 { get; set; }

    [Column(TypeName = "NUMBER(9,3)")]
    public decimal? TEILMENGE5 { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string ORI_KART_NR { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string EINGANG_KNZ { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string AUFBAU { get; set; }

    [Precision(5)]
    public short? TARA_TAGE { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string KART_NR_2 { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string KOMBI_KNZ { get; set; }

    [Precision(9)]
    public int? SPED_ID { get; set; }

    [Precision(9)]
    public int? FRZG_ID { get; set; }

    [ForeignKey("FRZG_ID")]
    [InverseProperty("KARTEN")]
    public virtual FAHRZEUGE FRZG { get; set; }

    [ForeignKey("SPED_ID")]
    [InverseProperty("KARTEN")]
    public virtual SPEDITIONEN SPED { get; set; }
}
