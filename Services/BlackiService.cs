using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.Entity;
using Radzen;
using QwTest7.Data;
using QwTest7.Models.Blacki;
using System.Linq.Dynamic.Core;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.InkML;

namespace QwTest7.Services
{
    /// <summary>
    /// Datenservice für QUVA
    /// </summary>
    public partial class BlackiService: BaseDbService
    {
        public BlackiService(BlackiContext ctx, NavigationManager navigationManager): base(ctx, navigationManager) 
        {
        }

        public BlackiContext AppCtx()
        {
            return (BlackiContext)Ctx;
        }
    }
}