﻿using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.Entity;
using Radzen;
using Query = Radzen.Query;
using QwTest7.Data;
using QwTest7.Models.Blacki;
using System.Linq.Dynamic.Core;

namespace QwTest7.Services
{
    /// <summary>
    /// Datenservice für QUVA
    /// </summary>
    public partial class BlackiService
    {
        public BlackiContext Ctx { get; set; }
        private readonly NavigationManager navigationManager;

        public BlackiService(BlackiContext ctx, NavigationManager navigationManager)
        {
            Ctx = ctx;
            this.navigationManager = navigationManager;
        }

        // von CRMDemoBlazor / dynamic-linq:
        public IQueryable QueryableFromQuery(Query query, IQueryable items)
        {
            if (query != null)
            {
                // Relationen laden (Eager loading)
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
            return items;
        }

        public Query QueryFromLoadDataArgs(LoadDataArgs args)
        {
            //dynamic-linq, siehe d:\Blazor\Repos\radzenhq\radzen-blazor\Radzen.Blazor\Common.cs
            //für LoadData Event
            var query = new Query
            {
                Skip = args.Skip,
                Top = args.Top,
                Filter = args.Filter,
                OrderBy = args.OrderBy
            };
            return query;
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
            var items = (IQueryable<FILTERABFRAGEN>)QueryableFromQuery(query, Ctx.FILTERABFRAGEN_Tbl);
            var fltr = items.FirstOrDefault();
            //Neuladen erzwingen:
            if (fltr != null)
                Ctx.Entry(fltr).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return fltr;  //null bei EOF
        }

        #endregion

        #region Entity

        public IQueryable<T> EntityQuery<T>(Query query) where T : class
        {
            var items = Ctx.Set<T>().AsQueryable();
            if (query != null)
            {
                items = (IQueryable<T>)QueryableFromQuery(query, items);
            }
            return items;
        }

        public int EntityQueryCount<T>(Query query) where T : class
        {
            var items = Ctx.Set<T>().AsQueryable();
            //items = items.Include(i => i.Contact);
            //items = items.Include(i => i.OpportunityStatus);
            if (query != null)
            {
                query.Skip = null;
                query.Top = null;
                items = (IQueryable<T>)QueryableFromQuery(query, items);
            }
            return items.Count();
        }

        public void EntityUpdate<T>(T entity) where T : class
        {
            Ctx.Update<T>(entity);
            Ctx.SaveChanges();
        }

        public EntityEntry EntityEntry<T>(T entity) where T : class
        {
            return Ctx.Entry<T>(entity);
        }

        public void EntityRemove<T>(T entity) where T : class
        {
            Ctx.Remove<T>(entity);
            Ctx.SaveChanges();
        }

        public void EntityAdd<T>(T entity) where T : class
        {
            Ctx.Add<T>(entity);
            Ctx.SaveChanges();
        }

        #endregion

    }
}