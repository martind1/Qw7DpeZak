using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
using Microsoft.EntityFrameworkCore;

namespace QwTest7.Models.KmpDb;

[Table("DATEIEN")]
[Index("ORDNER", "FILENAME", Name = "UK_DATN", IsUnique = true)]
public partial class DATEIEN
{
    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string ORDNER { get; set; }

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string FILENAME { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? FILETIME { get; set; }

    [Column(TypeName = "BLOB")]
    public byte[] INHALT { get; set; }

    [Key]
    [Precision(9)]
    public int DATN_ID { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string ERFASST_VON { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? ERFASST_AM { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string ERFASST_DATENBANK { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string GEAENDERT_VON { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? GEAENDERT_AM { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string GEAENDERT_DATENBANK { get; set; }

    [Precision(9)]
    public int? ANZAHL_AENDERUNGEN { get; set; }

    [StringLength(2000)]
    [Unicode(false)]
    public string BEMERKUNG { get; set; }
}
