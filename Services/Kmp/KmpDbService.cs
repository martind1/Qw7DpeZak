using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.Entity;
using Radzen;
using Query = Radzen.Query;
using QwTest7.Data;
using QwTest7.Models.Blacki;
using System.Linq.Dynamic.Core;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.InkML;
using QwTest7.Services.Kmp;

namespace QwTest7.Services.Kmp
{
    /// <summary>
    /// System Datenbank Service für Abfragen, Filter (andere Metadaten): KmpDbContext
    /// </summary>
    public partial class KmpDbService: BaseDbService
    {
        //todo: KmpDbContext
        public KmpDbService(KmpDbContext ctx, NavigationManager navigationManager): base(ctx, navigationManager) 
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

    }
}