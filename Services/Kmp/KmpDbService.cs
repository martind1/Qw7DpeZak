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

namespace QwTest7.Services.Kmp
{
    /// <summary>
    /// System Datenbank Service für Abfragen, Filter (andere Metadaten): KmpDbContext
    /// </summary>
    public partial class KmpDbService : BaseDbService
    {
        //todo: KmpDbContext
        public KmpDbService(KmpDbContext ctx, NavigationManager navigationManager, GlobalService gnav) :
            base(ctx, navigationManager)
        {
            Gnav = gnav;
        }

        public KmpDbContext AppCtx()
        {
            return (KmpDbContext)Ctx;
        }

        GlobalService Gnav { get; set; }


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

        private IEnumerable<INITIALISIERUNGEN> iniDB;
        protected IEnumerable<INITIALISIERUNGEN> IniDB { get => iniDB ?? LoadInidb(); set => iniDB = value; }

        /// <summary>
        /// ergibt Tabelle der für diesen User/Maschine/Anwendung/Vorgabe vorhandenen Einträge
        /// select distinct ANWENDUNG, TYP, NAME, SECTION from QUSY.INITIALISIERUNGEN R_INIT
        ///  where (ANWENDUNG = 'QUVAE')
        ///    and ((TYP = 'A') 
        ///     or  ((TYP = 'M') and (NAME = '0120')) 
        ///     or  ((TYP = 'U') and (NAME = 'MDAMBACH')) 
        ///     or  ((TYP = 'V') and (NAME LIKE '%')))
        ///  order by SECTION

        /// </summary>
        /// <returns></returns>
        protected IEnumerable<INITIALISIERUNGEN> LoadInidb()
        {
            var query = new Query();
            query.Filter = "FORM = @0 and NAME = @1";
            query.FilterParameters = new object[] { Gnav.AnweKennung, Gnav.MaschineName, Gnav.UserName };


            var items = (IQueryable<INITIALISIERUNGEN>)QueryableFromQuery(query, AppCtx().INITIALISIERUNGEN_Tbl);
            iniDB = items.ToList();
            return iniDB;
        }



        public string ReadItem(string section, string ident, string dflt)
        {
            return section + ident + dflt;
            //todo:linq
        }

        public int ReadItem(string section, string ident, int dflt)
        {
            return int.Parse(ident) + dflt + int.Parse(section);
            //todo:linq
        }

        #endregion

    }
}