/*
 * 23.09.23 md  Async
*/
using Microsoft.EntityFrameworkCore;
using Query = Radzen.Query;
using DpeZak.Database.Models;
using DpeZak.Services.Db;

namespace DpeZak.Services.Kmp
{
    /// <summary>
    /// System Datenbank Service für Abfragen, Filter (andere Metadaten): KmpDbContext
    /// </summary>
    public partial class KmpDbService : BaseDbService
    {
        public KmpDbService(DpeContext ctx) :
            base(ctx)
        {
        }

        public DpeContext AppCtx()
        {
            return (DpeContext)Ctx;
        }

        #region FLTR

        public async Task<FLTR?> GetFltr(string formkurz, string abfrage)
        {
            //var items = Ctx.FLTR.AsQueryable();
            var query = new Query()
            {
                Filter = "FORM = @0 and NAME = @1",
                FilterParameters = new object[] { formkurz, abfrage }
            };
            var items = QueryableFromQuery<FLTR>(query, AppCtx().FLTR);
            var fltr = await items.FirstOrDefaultAsync();
            //Neuladen erzwingen:
            if (fltr != null)
                Ctx.Entry(fltr).State = EntityState.Detached;
            return fltr;  //null bei EOF
        }

        #endregion

        #region Werkparameter
        #endregion

        #region IniDB

        /// <summary>
        /// ergibt Liste der für diesen User/Maschine/Anwendung/Vorgabe vorhandenen Einträge: SECTION, PARAM, WERT
        /// select distinct ANWENDUNG, TYP, NAME, SECTION from QUSY.R_INIT R_INIT
        ///  where (ANWENDUNG = 'QUVAE')
        ///    and ((TYP = 'A') 
        ///     or  ((TYP = 'M') and (NAME = '0120')) 
        ///     or  ((TYP = 'U') and (NAME = 'MDAMBACH')) 
        ///     or  ((TYP = 'V') and (NAME LIKE '%')))
        ///  order by TYP
        public async Task<List<R_INIT>> GetInitialisierungen(string anwekennung, string sectyp, string ininame)
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
            var items = QueryableFromQuery<R_INIT>(query, AppCtx().R_INIT);
            return await items.ToListAsync();
        }

        /// <summary>
        /// Speziel für Werkparameter: Eine Section einer Anwendung
        /// </summary>
        public async Task<List<R_INIT>> GetAnweSection(string anwekennung, string section)
        {
            var sectyp = "A";  //nur Typ=Anwendung
            var query = new Query
            {
                Filter = "ANWENDUNG = @0 and TYP = @1 and SECTION = @2",
                FilterParameters = new object[] { anwekennung, sectyp, section },
                OrderBy = "INIT_ID"
            };
            var items = QueryableFromQuery<R_INIT>(query, AppCtx().R_INIT);
            return await items.ToListAsync();
        }

        /// <summary>
        /// Insert oder Update ein Ini Eintrag
        /// </summary>
        /// <param name="ini"></param>
        public async Task SaveInitialisierungen(R_INIT ini)
        {
            var query = new Query();
            if (ini.TYP == "M" || ini.TYP == "U")
            {
                query.Filter = "ANWENDUNG = @0 and TYP = @1 and NAME = @2 and SECTION = @3 and PARAM = @4";
                query.FilterParameters = new object[] { ini.ANWENDUNG, ini.TYP, ini.NAME, ini.SECTION, ini.PARAM };
            }
            else
            {
                query.Filter = "ANWENDUNG = @0 and TYP = @1 and SECTION = @2 and PARAM = @3";
                query.FilterParameters = new object[] { ini.ANWENDUNG, ini.TYP, ini.SECTION, ini.PARAM };
            }
            var item = await EntityFirst<R_INIT>(query);

            if (item == null)
            {
                //erfassen
                await EntityAdd(ini);
            }
            else
            {
                //ändern
                item.WERT = ini.WERT;
                //await EntityUpdate(item);  //25.09.23 war ini
                await EntitySave();  //25.09.23 nur savechanges
            }
        }

        #endregion

    }

}