﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace DpeZak.Database.Models;

public partial class RInit
{
    public string Anwendung { get; set; } = null!;

    public string Typ { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Section { get; set; } = null!;

    public string Param { get; set; } = null!;

    public string Wert { get; set; } = null!;

    public int InitId { get; set; }

    public string? ErfasstVon { get; set; }

    public DateTime? ErfasstAm { get; set; }

    public string? ErfasstDatenbank { get; set; }

    public int? AnzahlAenderungen { get; set; }

    public DateTime? GeaendertAm { get; set; }

    public string? GeaendertVon { get; set; }

    public string? GeaendertDatenbank { get; set; }

    public string? Bemerkung { get; set; }
}