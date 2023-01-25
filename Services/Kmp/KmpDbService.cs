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
            var items = QueryableFromQuery<FILTERABFRAGEN>(query, AppCtx().FILTERABFRAGEN_Tbl);
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
        public IQueryable<INITIALISIERUNGEN> GetInitialisierungen(string anwekennung, string sectyp, string ininame)
        {
            var query = new Query();
            if (sectyp == "M" || sectyp == "U")
            {
                query.Filter = "ANWENDUNG = @0 and TYP = @1 and NAME = @2";
                query.FilterParameters = new object[] { anwekennung, sectyp, ininame };
            }
            else
            {
                query.Filter = "ANWENDUNG = @0 and TYP = @1";
                query.FilterParameters = new object[] { anwekennung, sectyp };
            }
            query.OrderBy = "SECTION, INIT_ID";
            var items = QueryableFromQuery<INITIALISIERUNGEN>(query, AppCtx().INITIALISIERUNGEN_Tbl);
            return items;
        }

        /// <summary>
        /// Insert oder Update ein Ini Eintrag
        /// </summary>
        /// <param name="ini"></param>
        public void SaveInitialisierungen(INITIALISIERUNGEN ini)
        {
            var query = new Query();
            if (ini.TYP == "M" || ini.TYP == "U")
            {
                query.Filter = "ANWENDUNG = @0 and TYP = @1 and NAME = @2 and SECTION = @3 and PARAM = @4";
                query.FilterParameters = new object[] { ini.ANWENDUNG, ini.TYP, ini.NAME, ini.SECTION, ini.PARAM};
            }
            else
            {
                query.Filter = "ANWENDUNG = @0 and TYP = @1 and SECTION = @2 and PARAM = @3";
                query.FilterParameters = new object[] { ini.ANWENDUNG, ini.TYP, ini.SECTION, ini.PARAM };
            }
            var item = EntityGet<INITIALISIERUNGEN>(query).FirstOrDefault();

            if (item == null) 
            {
                //erfassen
                EntityAdd<INITIALISIERUNGEN>(ini);
            }
            else
            {
                //ändern
                item.WERT = ini.WERT;
                EntityUpdate<INITIALISIERUNGEN>(ini);
            }
        }

        #endregion

    }

}