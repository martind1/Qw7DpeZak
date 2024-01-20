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

        #region Fltr

        public async Task<Fltr?> GetFltr(string formkurz, string abfrage)
        {
            //var items = Ctx.Fltr.AsQueryable();
            var query = new Query()
            {
                Filter = "FORM = @0 and Name = @1",
                FilterParameters = new object[] { formkurz, abfrage }
            };
            var items = QueryableFromQuery<Fltr>(query, AppCtx().Fltr);
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
        /// ergibt Liste der für diesen User/Maschine/Anwendung/Vorgabe vorhandenen Einträge: Section, Param, Wert
        /// select distinct Anwendung, Typ, Name, Section from QUSY.RInit RInit
        ///  where (Anwendung = 'QUVAE')
        ///    and ((Typ = 'A') 
        ///     or  ((Typ = 'M') and (Name = '0120')) 
        ///     or  ((Typ = 'U') and (Name = 'MDAMBACH')) 
        ///     or  ((Typ = 'V') and (Name LIKE '%')))
        ///  order by Typ
        public async Task<List<RInit>> GetInitialisierungen(string anwekennung, string sectyp, string ininame)
        {
            var query = new Query();
            if (sectyp == "M" || sectyp == "U")
            {
                query.Filter = "Anwendung = @0 and Typ = @1 and Name = @2";
                query.FilterParameters = new object[] { anwekennung, sectyp, ininame };
            }
            else
            {
                query.Filter = "Anwendung = @0 and Typ = @1";
                query.FilterParameters = new object[] { anwekennung, sectyp };
            }
            query.OrderBy = "Section, INIT_ID";
            var items = QueryableFromQuery<RInit>(query, AppCtx().RInit);
            return await items.ToListAsync();
        }

        /// <summary>
        /// Speziel für Werkparameter: Eine Section einer Anwendung
        /// </summary>
        public async Task<List<RInit>> GetAnweSection(string anwekennung, string section)
        {
            var sectyp = "A";  //nur Typ=Anwendung
            var query = new Query
            {
                Filter = "Anwendung = @0 and Typ = @1 and Section = @2",
                FilterParameters = new object[] { anwekennung, sectyp, section },
                OrderBy = "INIT_ID"
            };
            var items = QueryableFromQuery<RInit>(query, AppCtx().RInit);
            return await items.ToListAsync();
        }

        /// <summary>
        /// Insert oder Update ein Ini Eintrag
        /// </summary>
        /// <param name="ini"></param>
        public async Task SaveInitialisierungen(RInit ini)
        {
            var query = new Query();
            if (ini.Typ == "M" || ini.Typ == "U")
            {
                query.Filter = "Anwendung = @0 and Typ = @1 and Name = @2 and Section = @3 and Param = @4";
                query.FilterParameters = new object[] { ini.Anwendung, ini.Typ, ini.Name, ini.Section, ini.Param };
            }
            else
            {
                query.Filter = "Anwendung = @0 and Typ = @1 and Section = @2 and Param = @3";
                query.FilterParameters = new object[] { ini.Anwendung, ini.Typ, ini.Section, ini.Param };
            }
            var item = await EntityFirst<RInit>(query);

            if (item == null)
            {
                //erfassen
                await EntityAdd(ini);
            }
            else
            {
                //ändern
                item.Wert = ini.Wert;
                //await EntityUpdate(item);  //25.09.23 war ini
                await EntitySave();  //25.09.23 nur savechanges
            }
        }

        #endregion

    }

}