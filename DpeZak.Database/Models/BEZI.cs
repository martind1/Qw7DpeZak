﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace DpeZak.Database.Models;

public partial class Bezi
{
    public string BeziNr { get; set; } = null!;

    public string? BeziBez { get; set; }

    public string? VbezNr { get; set; }

    public short? ZoneNr { get; set; }

    public string? ErfasstVon { get; set; }

    public DateTime? ErfasstAm { get; set; }

    public string? GeaendertVon { get; set; }

    public DateTime? GeaendertAm { get; set; }

    public int? AnzahlAenderungen { get; set; }

    public string? Bemerkung { get; set; }

    public virtual ICollection<Kund> Kund { get; set; } = new List<Kund>();

    public virtual Vbez? VbezNrNavigation { get; set; }
}