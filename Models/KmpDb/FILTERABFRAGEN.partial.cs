using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using QwTest7.Services.Kmp;

namespace QwTest7.Models.KmpDb;

public partial class FILTERABFRAGEN
{
    //berechnete Eigenschaften
    //keine Spaltennamen (auch nicht als Lowercase)! Deshalb Prefix 'cf':
    [NotMapped]
    public ColumnList cfColumnlist { get => new(COLUMNLIST); }

    [NotMapped]
    public FltrList cfFltrlist { get => new(FLTRLIST); }
}
