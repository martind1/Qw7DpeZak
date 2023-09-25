namespace QwTest7.Portal.Services.Kmp
{
    public class Asws
    {
        public string Value { get; set; }
        public string Display { get; set; }
        //für Ceckbox:
        //public bool? this[string index]
        //{
        //    get { return index == "J" ? true : index == "N" ? false : null; }
        //    set { }
        //}
    }


    /// <summary>
    /// Feste Auswahlen Value=Gespeicherter Wert, Display=angezeigter Wert
    /// </summary>
    public static class Auswahl
    {

        public static readonly IEnumerable<Asws> aswOK = new Asws[] {
            new Asws(){ Value = "J", Display = "OK" },
            new Asws(){ Value = "N", Display = "Nicht OK" } };

        public static readonly IEnumerable<Asws> aswOKStrich = new Asws[] {
            new Asws(){ Value = "J", Display = "OK" },
            new Asws(){ Value = "N", Display = "-" } };

        public static readonly IEnumerable<Asws> aswLityp = new Asws[] {
            new Asws(){ Value = "A", Display = "Entsorgung" },
            new Asws(){ Value = "B", Display = "Abgänge" },
            new Asws(){ Value = "W", Display = "Intern" } };

        public static readonly IEnumerable<Asws> aswHtmlSingle = new Asws[] {
            new Asws(){ Value = "VFUE", Display = "Verfüllabschnitt" },
            new Asws(){ Value = "DKAT", Display = "Deponiekataster" },
            new Asws(){ Value = "PROB", Display = "Probenahme" },
            new Asws(){ Value = "FAHR", Display = "Fahrzeug" },
            new Asws(){ Value = "EDTM", Display = "Eingang Zeit" },
            new Asws(){ Value = "VONR", Display = "Beleg Nr." },
            new Asws(){ Value = "LORT", Display = "Lager" },
            new Asws(){ Value = "KATA", Display = "Kataster" },
            new Asws(){ Value = "CHAR", Display = "Kompost Chargennr." },
            new Asws(){ Value = "MKEN", Display = "Materialkennung" },
            new Asws(){ Value = "SRTE", Display = "Sorte" },
            new Asws(){ Value = "AVV", Display = "AVV" },
            new Asws(){ Value = "ENTS", Display = "Nachweis" },
            new Asws(){ Value = "BEF", Display = "Beförderer" },
            new Asws(){ Value = "ANF", Display = "Anfallstelle" },
        };
    }
}
