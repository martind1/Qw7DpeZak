/*
 * 23.09.23 md  Async
*/
using DpeZak.Database.Models;
using Serilog;
using System.Globalization;

namespace DpeZak.Services.Kmp;

public class IniDbService
{
    private IDictionary<IniKeyEntry, string>? anweList = null;
    private IDictionary<IniKeyEntry, string>? maschineList = null;
    private IDictionary<IniKeyEntry, string>? userList = null;
    private IDictionary<IniKeyEntry, string>? vorgabeList = null;
    public string Anwe = string.Empty;
    public string Maschine = string.Empty;
    public string User = string.Empty;
    public string Vorgabe = string.Empty;  //n.b. für spätere Erweiterung

    public IniDbService(KmpDbService svc, GlobalService gnav)
    {
        Svc = svc;
        Gnav = gnav;
        Gnav.OnAnweChanged += GnavAnweChanged;
        Gnav.OnMaschineChanged += GnavMaschineChanged;
        Gnav.OnUserChanged += GnavUserChanged;
        GnavAnweChanged();
        GnavMaschineChanged();
        GnavUserChanged();
    }

    private KmpDbService Svc { get; set; }
    private GlobalService Gnav { get; set; }

    private IDictionary<IniKeyEntry, string> AnweList
    {
        get => anweList ??= GetIniList(Anwe, SecTyp.Anwendung, Anwe);
    }
    private IDictionary<IniKeyEntry, string> MaschineList
    {
        get => maschineList ??= GetIniList(Anwe, SecTyp.Maschine, Maschine);
    }
    private IDictionary<IniKeyEntry, string> UserList
    {
        get => userList ??= GetIniList(Anwe, SecTyp.User, User);
    }
    private IDictionary<IniKeyEntry, string> VorgabeList
    {
        get => vorgabeList ??= GetIniList(Anwe, SecTyp.Vorgabe, Vorgabe);
    }

    private IDictionary<IniKeyEntry, string> GetIniList(string anwekennung, SecTyp sectyp, string ininame)
    {
        string sectypstr = SecTypToString(sectyp);
        Log.Information($"## GetIniList({anwekennung}, {sectypstr}, {ininame})");
        var items = Svc.GetInitialisierungen(anwekennung, sectypstr, ininame).GetAwaiter().GetResult();
        var dic = new Dictionary<IniKeyEntry, string>();
        foreach (var item in items)
        {
            try
            {
                dic.Add(new IniKeyEntry(item.SECTION, item.PARAM), item.WERT);
            }
            catch (Exception ex)
            {
                Log.Warning($"GetIniList({anwekennung},{sectyp},{ininame}) ({item.SECTION}, {item.PARAM}, {item.WERT})", ex);
            }
        }
        return dic;
    }

    private void GnavAnweChanged()
    {
        anweList = null;
        maschineList = null;
        userList = null;
        vorgabeList = null;
        sectionTyp = null;       //neu laden
        Anwe = Gnav.IniAnwe;
        Vorgabe = Gnav.IniAnwe;  //für spätere Erweiterung
    }
    private void GnavMaschineChanged()
    {
        maschineList = null;
        sectionTyp = null;
        Maschine = Gnav.MaschineName;
    }
    private void GnavUserChanged()
    {
        userList = null;
        sectionTyp = null;
        User = Gnav.UserName;
    }

    /// <summary>
    /// distinct section über die einzelnen Listen: Anwe überschreibt Maschine überschreibt User..
    /// </summary>
    private SectionTypes LoadSectionTypes()
    {
        var sectionTypes = new SectionTypes();
        var stl = new List<SecTyp>() { SecTyp.Vorgabe, SecTyp.User, SecTyp.Maschine, SecTyp.Anwendung };
        foreach (var t in stl)
        {
            var qlist = t switch
            {
                SecTyp.Vorgabe => VorgabeList.Keys,
                SecTyp.User => UserList.Keys,
                SecTyp.Maschine => MaschineList.Keys,
                SecTyp.Anwendung => AnweList.Keys,
                _ => throw new NotImplementedException()
            };
            var query = qlist.Select(i => i.SECTION).Distinct();
            foreach (var item in query)
            {
                //Reihenfolge ist wichtig!: Anwe überschreibt Maschine ü User ü Vorgabe
                sectionTypes[item] = t;
            }
        }
        return sectionTypes;
    }

    #region öffentliche Aufrufe

    /// <summary>
    /// SectionTyp["mysect"] = SecTyp.Maschine;
    /// SecTyp st = SectionTyp["mysect"]
    /// </summary>
    public SectionTypes SectionTyp
    {
        get => sectionTyp ??= LoadSectionTypes();
    }
    private SectionTypes? sectionTyp = null;

    public string ReadItem(string section, string ident, string dflt)
    {
        // dynamisch bestimmen pro ident: zu langsam, nicht kompatibel
        //if (!AnweList.TryGetValue(new IniKeyEntry(section, ident), out string result))
        //{
        //    if (!MaschineList.TryGetValue(new IniKeyEntry(section, ident), out result))
        //    {
        //        if (!UserList.TryGetValue(new IniKeyEntry(section, ident), out result))
        //        {
        //            if (!VorgabeList.TryGetValue(new IniKeyEntry(section, ident), out result))
        //            {
        //                result = dflt;
        //            }

        //        }

        //    }

        //}
        SecTyp sectyp = SectionTyp[section];
        string? result = dflt;
        switch (sectyp)
        {
            case SecTyp.Vorgabe:
                if (!VorgabeList.TryGetValue(new IniKeyEntry(section, ident), out result))
                {
                    result = dflt;
                }
                break;
            case SecTyp.User:
                if (!UserList.TryGetValue(new IniKeyEntry(section, ident), out result))
                {
                    result = dflt;
                }
                break;
            case SecTyp.Maschine:
                if (!MaschineList.TryGetValue(new IniKeyEntry(section, ident), out result))
                {
                    result = dflt;
                }
                break;
            case SecTyp.Anwendung:
                if (!AnweList.TryGetValue(new IniKeyEntry(section, ident), out result))
                {
                    result = dflt;
                }
                break;
        }
        return result.Trim();
    }

    private readonly string magic = "{D50CD510-E76A-40A1-9AF5-51466392824B}";

    public int ReadItem(string section, string ident, int dflt)
    {
        int result;
        bool ishex = false;
        string s = ReadItem(section, ident, magic);
        if (s == magic)
        {
            result = dflt;
        }
        else
        {
            if (s.StartsWith("0x"))
            {
                s = s[2..];
                ishex = true;
            }
            else if (s.StartsWith("$"))
            {
                s = s[1..];
                ishex = true;
            }
            if (!(ishex
                ? int.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result)
                : int.TryParse(s, out result)))
                result = 0;
        }
        return result;
    }


    public async Task WriteItem(SecTyp sectyp, string section, string ident, string value)
    {
        var init = new INITIALISIERUNGEN()
        {
            ANWENDUNG = Anwe,
            TYP = SecTypToString(sectyp),
            NAME = sectyp switch
            {
                SecTyp.User => User,
                SecTyp.Maschine => Maschine,
                _ => Anwe
            },
            SECTION = section,
            PARAM = ident,
            WERT = value
        };
        await Svc.SaveInitialisierungen(init);
    }

    public async Task WriteItem(string section, string ident, string value)
    {
        SecTyp sectyp = SectionTyp[section];
        await WriteItem(sectyp, section, ident, value);
    }

    public async Task WriteItem(string section, string ident, int value)
    {
        await WriteItem(section, ident, value.ToString());
    }

    #endregion

    #region Hilfsfunktionen

    public static string SecTypToString(SecTyp sectyp)
    {
        //return Convert.ToChar(int.Parse(sectyp.ToString("D"))).ToString();  //User->85->'U'->"U"
        string result = sectyp switch
        {
            SecTyp.Vorgabe => "V",
            SecTyp.User => "U",
            SecTyp.Maschine => "M",
            SecTyp.Anwendung => "A",
            _ => throw new NotImplementedException()
        };
        return result;
    }

    public static SecTyp StringToSecTyp(string str)
    {
        //"U"->'U'->85->User
        //byte[] asciiBytes = Encoding.ASCII.GetBytes(str);
        //return (SecTyp)asciiBytes[0];
        SecTyp result = str switch
        {
            "V" => SecTyp.Vorgabe,
            "U" => SecTyp.User,
            "M" => SecTyp.Maschine,
            "A" => SecTyp.Anwendung,
            _ => throw new NotImplementedException()
        };
        return result;

    }

    #endregion

    #region Listen für Interne Test Seiten

    /// <summary>
    /// ergibt Daten für Anzeige auf Page
    /// Es werden nur die Felder SECTION und TYP befüllt
    /// </summary>
    public IList<INITIALISIERUNGEN> SectionTypSet()
    {
        var result = new List<INITIALISIERUNGEN>();
        foreach (var ini in SectionTyp.SecTypList)
        {
            result.Add(new INITIALISIERUNGEN() { SECTION = ini.Key, TYP = SecTypToString(ini.Value) });
        }

        return result;
    }

    /// <summary>
    /// ergibt Daten für Anzeige auf Page
    /// Es werden nur die Felder SECTION, PARAM und WERT befüllt
    /// </summary>
    public IList<INITIALISIERUNGEN> AnweSet()
    {
        var result = new List<INITIALISIERUNGEN>();
        foreach (var ini in AnweList)
        {
            result.Add(new INITIALISIERUNGEN() { SECTION = ini.Key.SECTION, PARAM = ini.Key.PARAM, WERT = ini.Value });
        }
        return result;
    }
    public IList<INITIALISIERUNGEN> MaschineSet()
    {
        var result = new List<INITIALISIERUNGEN>();
        foreach (var ini in MaschineList)
        {
            result.Add(new INITIALISIERUNGEN() { SECTION = ini.Key.SECTION, PARAM = ini.Key.PARAM, WERT = ini.Value });
        }
        return result;
    }
    public IList<INITIALISIERUNGEN> UserSet()
    {
        var result = new List<INITIALISIERUNGEN>();
        foreach (var ini in UserList)
        {
            result.Add(new INITIALISIERUNGEN() { SECTION = ini.Key.SECTION, PARAM = ini.Key.PARAM, WERT = ini.Value });
        }
        return result;
    }
    public IList<INITIALISIERUNGEN> VorgabeSet()
    {
        var result = new List<INITIALISIERUNGEN>();
        foreach (var ini in VorgabeList)
        {
            result.Add(new INITIALISIERUNGEN() { SECTION = ini.Key.SECTION, PARAM = ini.Key.PARAM, WERT = ini.Value });
        }
        return result;
    }

    #endregion

    #region Spezial zB für Werkparameter

    /// <summary>
    /// Ergibt alle Einträge einer Section anhand Anwe und Sectionname, Typ=Anwendung
    /// </summary>
    public async Task<IDictionary<IniKeyEntry, string>> GetSectionParameter(string Anwe, string Section)
    {
        var items = await Svc.GetAnweSection(Anwe, Section);
        var dic = new Dictionary<IniKeyEntry, string>();
        foreach (var item in items)
        {
            try
            {
                dic.Add(new IniKeyEntry(item.SECTION, item.PARAM), item.WERT);
            }
            catch (Exception ex)
            {
                Log.Warning($"GetSectionParameter({Anwe},{Section}) ({item.SECTION}, {item.PARAM}, {item.WERT})", ex);
            }
        }
        return dic;
    }

    /// <summary>
    /// ergibt Daten für Anzeige auf Page
    /// Es werden nur die Felder SECTION, PARAM und WERT befüllt
    /// </summary>
    public async Task<IList<INITIALISIERUNGEN>> SectionParameterSet(string Anwe, string Section)
    {
        var items = await Svc.GetAnweSection(Anwe, Section);
        return items;
    }



    #endregion



}

public enum SecTyp
{
    Anwendung = 'A',
    Maschine = 'M',
    User = 'U',
    Vorgabe = 'V'
}

public partial class IniKeyEntry
{
    public string SECTION { get; set; }
    public string PARAM { get; set; }
    //public string WERT { get; set; }

    public IniKeyEntry(string section, string param)
    {
        SECTION = section;
        PARAM = param;
        //WERT = wert;
    }
}

/// <summary>
/// Schnellzugriff auf den SectionTyp pro Section. Wird beim Laden befüllt.
/// </summary>
public class SectionTypes
{
    public SectionTypes()
    {
        SecTypList = new Dictionary<string, SecTyp>();
    }

    public SecTyp this[string section]
    {
        get
        {
            if (!SecTypList.TryGetValue(section, out SecTyp result))
            {
                result = SecTyp.User;  //Standardwert wenn kein Eintrag
            }
            return result;
        }
        set
        {
            SecTypList[section] = value;
        }
    }

    public IDictionary<string, SecTyp> SecTypList;

}

public class SectionTypEntry
{
    public string SECTION { get; set; }
    public string PARAM { get; set; }
}
