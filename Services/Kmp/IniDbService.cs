using Microsoft.AspNetCore.Components;
using QwTest7.Models.KmpDb;
using System.Globalization;

namespace QwTest7.Services.Kmp
{
    public class IniDbService
    {
        private IDictionary<IniKeyEntry, string> anweList = null;
        private IDictionary<IniKeyEntry, string> maschineList = null;
        private IDictionary<IniKeyEntry, string> userList = null;
        private IDictionary<IniKeyEntry, string> vorgabeList = null;
        private string anwe = null;
        private string maschine = null;
        private string user = null;
        private string vorgabe = null;  //n.b. für spätere Erweiterung

        public IniDbService(KmpDbService svc, GlobalService gnav)
        {
            Svc = svc;
            Gnav = gnav;
            Gnav.OnAnweChanged += GnavAnweChanged;
            Gnav.OnMaschineChanged += GnavMaschineChanged;
            Gnav.OnUserChanged += GnavUserChanged;
        }

        private KmpDbService Svc { get; set; }
        private GlobalService Gnav { get; set; }

        private IDictionary<IniKeyEntry, string> AnweList
        {
            get => anweList ??= Svc.GetIniList(anwe, SecTyp.Anwendung, anwe);
        }
        private IDictionary<IniKeyEntry, string> MaschineList
        {
            get => maschineList ??= Svc.GetIniList(anwe, SecTyp.Maschine, maschine);
        }
        private IDictionary<IniKeyEntry, string> UserList
        {
            get => userList ??= Svc.GetIniList(anwe, SecTyp.User, user);
        }
        private IDictionary<IniKeyEntry, string> VorgabeList
        {
            get => vorgabeList ??= Svc.GetIniList(anwe, SecTyp.Vorgabe, vorgabe);
        }

        private void GnavAnweChanged()
        {
            anweList = null;
            sectionTyp = null;
            anwe = Gnav.AnweKennung;
            vorgabe = Gnav.AnweKennung;  //für spätere Erweiterung
        }
        private void GnavMaschineChanged()
        {
            maschineList = null;
            sectionTyp = null;
            maschine = Gnav.MaschineName;
        }
        private void GnavUserChanged()
        {
            userList = null;
            sectionTyp = null;
            user = Gnav.UserName;
        }

        private SectionTypes sectionTyp = null;

        private SectionTypes LoadSectionTypes()
        {
            SectionTypes sectionTypes = new();
            //distinct section über die einzelnen Listen:
            for (var i = 0; i < 4; i++)
            {
                var qlist = i switch
                {
                    0 => VorgabeList.Keys,
                    1 => UserList.Keys,
                    2 => MaschineList.Keys,
                    3 => AnweList.Keys,
                    _ => throw new NotImplementedException()
                };
                var query = qlist.Select(i => i.SECTION).Distinct();
                foreach (var item in query)
                {
                    sectionTypes[item] = SecTyp.Vorgabe;
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

        public string ReadItem(string section, string ident, string dflt)
        {
            string result;
            if (!AnweList.TryGetValue(new IniKeyEntry(section, ident), out result))
            {
                if (!MaschineList.TryGetValue(new IniKeyEntry(section, ident), out result))
                {
                    if (!UserList.TryGetValue(new IniKeyEntry(section, ident), out result))
                    {
                        if (!VorgabeList.TryGetValue(new IniKeyEntry(section, ident), out result))
                        {
                            result = dflt;
                        }

                    }

                }

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
                if (!(ishex ? int.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result)
                    : int.TryParse(s, out result)))
                    result = 0;
            }
            return result;
        }


        public void WriteItem(SecTyp sectyp, string section, string ident, string value)
        {
            INITIALISIERUNGEN init = new INITIALISIERUNGEN()
            {
                ANWENDUNG = anwe,
                TYP = KmpDbService.SecTypToString(sectyp),
                NAME = sectyp switch
                {
                    SecTyp.User => user,
                    SecTyp.Maschine => maschine,
                    _ => anwe
                },
                SECTION = section,
                PARAM = ident,
                WERT = value
            };
        }

        public void WriteItem(string section, string ident, string value)
        {
            SecTyp sectyp = SecTyp.User;
        }

        public void WriteItem(string section, string ident, int value)
        {
            SecTyp sectyp = SecTyp.User;
        }

        //string result;
        //if (!AnweList.TryGetValue(new IniKeyEntry(section, ident), out result))
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


        #endregion

    }


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
}
