using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using Radzen;
using Query = Radzen.Query;
using QwTest7.Data;
using QwTest7.Models.Blacki;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace QwTest7.Services
{
    /// <summary>
    /// Datenservice für QUVA
    /// </summary>
    public partial class BaseDbService
    {
        public Microsoft.EntityFrameworkCore.DbContext Ctx { get; set; }
        private readonly NavigationManager navigationManager;

        public BaseDbService(Microsoft.EntityFrameworkCore.DbContext ctx,
            NavigationManager navigationManager)
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
            Ctx.Update(entity);
            Ctx.SaveChanges();
        }

        public EntityEntry EntityEntry<T>(T entity) where T : class
        {
            return Ctx.Entry(entity);
        }

        public void EntityRemove<T>(T entity) where T : class
        {
            Ctx.Remove(entity);
            Ctx.SaveChanges();
        }

        public void EntityAdd<T>(T entity) where T : class
        {
            Ctx.Add(entity);
            Ctx.SaveChanges();
        }
        #endregion

        #region Studio Funktionen

        public async Task<IQueryable<T>> EntityGet<T>(Query query = null) where T : class
        {
            var items = Ctx.Set<T>().AsQueryable();
            if (query != null)
            {
                items = (IQueryable<T>)QueryableFromQuery(query, items);
            }
            return await Task.FromResult(items);
        }

        public async Task<T> EntityDelete<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new Exception("Item no longer available");
            }


            Ctx.Remove(entity);
            try
            {
                Ctx.SaveChanges();
            }
            catch
            {
                Ctx.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                throw;
            }

            return entity;
        }


        #endregion

    }
}