using QwTest7.Portal.Services.Kmp;
using Serilog;

namespace QwTest7.Services.Kmp
{
    // Datenstrukturen für LNav, GNav:
    // Columnlist: Metadaten für Grid Column
    // FltrList: Metadaten für Suchkriterien / .where
    // (SqlFieldList: Metadaten für Feldliste / .insert)
    // (FormatList Aufbau: <Fieldname>=<Format> # Format = [r,][R,])


    /// <summary>
    /// FltrList: Verbindung zur KMP Welt. Für SQL Where Clause
    /// </summary>
    public partial class FltrList
    {
        public List<FltrListItem> Fltrs { get; set; }

        public FltrList()
        {
            Fltrs = new List<FltrListItem>();
            SqlParams = new Dictionary<int, object>();
        }

        public FltrList(string fltrlist) : this()
        {
            //fltrlist Zeile idF <FieldName>=<Suchkriterien>
            if (fltrlist != null)
            {
                List<string> list = new(
                    fltrlist.Split(new string[] { "\r\n", "\n", "\r" },
                    StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));
                foreach (var s in list)
                {
                    if (!s.StartsWith(";"))
                    {
                        var SL = s.Split2("=");  //beware ANL_NA1=AGH%;=
                        if (SL.Length >= 2 && !String.IsNullOrEmpty(SL[1]))
                        {
                            var fli = new FltrListItem(SL[0], SL[1]);
                            Fltrs.Add(fli);
                        }
                    }
                }
            }
        }
    }


    // Beschreibung Suchkriterien eines Felds
    // KmpStr (von Kmp.Fltrlist.Zeile): a >a <a == >= a;b;c [raw Feld-Argument] {raw Zeile ohne Feld}
    // Where (für Linq): <FldName> = "a" oder <FldName> > b
    // OrFlag (Kmp beginnt mit ';'): true = Verknüpfung mit 'or' mit anderen Feldern (und entspr Klammerung)
    public partial class FltrListItem
    {
        public string KmpStr { get; set; }
        public string Fieldname { get; set; }

        public string SqlWhere { get; set; }

        //Item anhand KMP Beschreibung anlegen. Noch kein Where/SQL
        public FltrListItem(string fieldname, string kmpstr)
        {
            Fieldname = fieldname;
            KmpStr = kmpstr;
        }
    }

    
}
