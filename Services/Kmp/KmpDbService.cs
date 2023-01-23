using Serilog;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.Entity;
using Radzen;
using Query = Radzen.Query;
using QwTest7.Data;
using QwTest7.Models.KmpDb;
using System.Linq.Dynamic.Core;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.InkML;
using QwTest7.Services.Kmp;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QwTest7.Services.Kmp
{
    /// <summary>
    /// System Datenbank Service für Abfragen, Filter (andere Metadaten): KmpDbContext
    /// </summary>
    public partial class KmpDbService : BaseDbService
    {
        public KmpDbService(KmpDbContext ctx, NavigationManager navigationManager) :
            base(ctx, navigationManager)
        {
        }

        public KmpDbContext AppCtx()
        {
            return (KmpDbContext)Ctx;
        }

        #region FILTERABFRAGEN

        public FILTERABFRAGEN GetFltr(string formkurz, string abfrage)
        {
            //var items = Ctx.FLTR_Tbl.AsQueryable();
            var query = new Query()
            {
                Filter = "FORM = @0 and NAME = @1",
                FilterParameters = new object[] { formkurz, abfrage }
            };
            var items = (IQueryable<FILTERABFRAGEN>)QueryableFromQuery(query, AppCtx().FILTERABFRAGEN_Tbl);
            var fltr = items.FirstOrDefault();
            //Neuladen erzwingen:
            if (fltr != null)
                Ctx.Entry(fltr).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return fltr;  //null bei EOF
        }

        #endregion

        #region Werkparameter
        #endregion

        #region IniDB

        /// <summary>
        /// ergibt Liste der für diesen User/Maschine/Anwendung/Vorgabe vorhandenen Einträge: SECTION, PARAM, WERT
        /// select distinct ANWENDUNG, TYP, NAME, SECTION from QUSY.INITIALISIERUNGEN R_INIT
        ///  where (ANWENDUNG = 'QUVAE')
        ///    and ((TYP = 'A') 
        ///     or  ((TYP = 'M') and (NAME = '0120')) 
        ///     or  ((TYP = 'U') and (NAME = 'MDAMBACH')) 
        ///     or  ((TYP = 'V') and (NAME LIKE '%')))
        ///  order by TYP
        public IDictionary<IniKeyEntry, string> GetIniList(string anwekennung, SecTyp sectyp, string ininame)
        {
            var query = new Query();
            string sectypstr = SecTypToString(sectyp);
            if (sectyp == SecTyp.Maschine || sectyp == SecTyp.User)
            {
                query.Filter = "ANWENDUNG = @0 and TYP = @1 and NAME = @2";
                query.FilterParameters = new object[] { anwekennung, sectypstr, ininame };
            }
            else
            {
                query.Filter = "ANWENDUNG = @0 and TYP = @1";
                query.FilterParameters = new object[] { anwekennung, sectypstr };
            }
            query.OrderBy = "SECTION, INIT_ID";
            var items = (IQueryable<INITIALISIERUNGEN>)QueryableFromQuery(query, AppCtx().INITIALISIERUNGEN_Tbl);
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

        public void SaveIni(INITIALISIERUNGEN ini)
        {
            var query = new Query();
            SecTyp sectyp = StringToSecTyp(ini.TYP);
            if (sectyp == SecTyp.Maschine || sectyp == SecTyp.User)
            {
                query.Filter = "ANWENDUNG = @0 and TYP = @1 and NAME = @2 and SECTION = @3 and PARAM = @4";
                query.FilterParameters = new object[] { ini.ANWENDUNG, ini.TYP, ini.NAME, ini.SECTION, ini.PARAM};
            }
            else
            {
                query.Filter = "ANWENDUNG = @0 and TYP = @1 and SECTION = @2 and PARAM = @3";
                query.FilterParameters = new object[] { ini.ANWENDUNG, ini.TYP, ini.SECTION, ini.PARAM };
            }
            var items = (IQueryable<INITIALISIERUNGEN>)QueryableFromQuery(query, AppCtx().INITIALISIERUNGEN_Tbl);
            var item = items.FirstOrDefault();
            if (item == null) 
            {
                //erfassen
            }
            else
            {
                //ändern
            }
        }

        public static string SecTypToString(SecTyp sectyp)
        {
            return Convert.ToChar(int.Parse(sectyp.ToString("D"))).ToString();  //User->85->'U'->"U"
        }

        public static SecTyp StringToSecTyp(string str)
        {
            //"U"->'U'->85->User
            byte[] asciiBytes = Encoding.ASCII.GetBytes(str);
            return (SecTyp)asciiBytes[0];
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

}