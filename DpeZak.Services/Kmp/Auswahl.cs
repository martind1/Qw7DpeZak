namespace DpeZak.Services.Kmp
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
            new(){ Value = "J", Display = "OK" },
            new(){ Value = "N", Display = "Nicht OK" } };

        public static readonly IEnumerable<Asws> aswOKStrich = new Asws[] {
            new(){ Value = "J", Display = "OK" },
            new(){ Value = "N", Display = "-" } };

        public static readonly IEnumerable<Asws> aswLityp = new Asws[] {
            new(){ Value = "A", Display = "Entsorgung" },
            new(){ Value = "B", Display = "Abgänge" },
            new(){ Value = "W", Display = "Intern" } };

        public static readonly IEnumerable<Asws> aswHtmlSingle = new Asws[] {
            new(){ Value = "VFUE", Display = "Verfüllabschnitt" },
            new(){ Value = "DKAT", Display = "Deponiekataster" },
            new(){ Value = "PROB", Display = "Probenahme" },
            new(){ Value = "FAHR", Display = "Fahrzeug" },
            new(){ Value = "EDTM", Display = "Eingang Zeit" },
            new(){ Value = "VONR", Display = "Beleg Nr." },
            new(){ Value = "LORT", Display = "Lager" },
            new(){ Value = "KATA", Display = "Kataster" },
            new(){ Value = "CHAR", Display = "Kompost Chargennr." },
            new(){ Value = "MKEN", Display = "Materialkennung" },
            new(){ Value = "SRTE", Display = "Sorte" },
            new(){ Value = "AVV", Display = "AVV" },
            new(){ Value = "ENTS", Display = "Nachweis" },
            new(){ Value = "BEF", Display = "Beförderer" },
            new(){ Value = "ANF", Display = "Anfallstelle" },
        };
    }
}
