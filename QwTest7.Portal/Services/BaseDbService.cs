/*
 * 23.09.23 md  only async 
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Radzen;
using System.Linq.Dynamic.Core;

namespace QwTest7.Portal.Services
{
    /// <summary>
    /// Datenservice für QUVA
    /// </summary>
    public partial class BaseDbService
    {
        public DbContext Ctx { get; set; }

        public BaseDbService(DbContext ctx)
        {
            Ctx = ctx;
        }


        // von CRMDemoBlazor / dynamic-linq:
        //public IQueryable<T> QueryableFromQuery<T>(Query query, IQueryable<T> items) where T : class
        public IQueryable<T> QueryableFromQuery<T>(Query query, IQueryable<T> items) where T : class
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
            //items = items.AsNoTracking();  //25.09.23
            return items;
        }

        public Query QueryFromLoadDataArgs(LoadDataArgs args)
        {
            //dynamic-linq, siehe https://github.com/radzenhq/radzen-blazor/blob/master/Radzen.Blazor/Common.cs
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

        public IQueryable<T> EntityGet<T>(Query query) where T : class
        {
            var items = Ctx.Set<T>().AsQueryable();
            if (query != null)
            {
                items = QueryableFromQuery(query, items);
            }
            return items;
        }

        public async Task<T> EntityFirst<T>(Query query) where T : class
        {
            var items = Ctx.Set<T>().AsQueryable();
            if (query != null)
            {
                items = QueryableFromQuery(query, items);
            }
            var entity = await items.FirstOrDefaultAsync();
            return entity;
        }

        public async Task<List<T>> EntityList<T>(Query query) where T : class
        {
            var items = Ctx.Set<T>().AsQueryable();
            if (query != null)
            {
                items = QueryableFromQuery(query, items);
            }
            var list = await items.ToListAsync();
            return list;
        }

        public async Task<int> EntityQueryCount<T>(Query query) where T : class
        {
            var items = Ctx.Set<T>().AsQueryable();
            //items = items.Include(i => i.Contact);
            //items = items.Include(i => i.OpportunityStatus);
            if (query != null)
            {
                var oldSkip = query.Skip;
                var oldTop = query.Top;
                try
                {
                    query.Skip = null;
                    query.Top = null;
                    items = QueryableFromQuery(query, items);
                }
                finally
                {
                    query.Skip = oldSkip;
                    query.Top = oldTop;
                }
            }
            return await Task.FromResult(items.Count());
        }


        public async Task EntityUpdate<T>(T entity) where T : class
        {
            Ctx.Update(entity);
            await Ctx.SaveChangesAsync();
        }

        public EntityEntry EntityEntry<T>(T entity) where T : class
        {
            return Ctx.Entry(entity);
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
                Ctx.Entry(entity).State = EntityState.Unchanged;
                throw;
            }
            return entity;
        }

        public async Task EntityAdd<T>(T entity) where T : class
        {
            Ctx.Add(entity);
            await Ctx.SaveChangesAsync();
        }

        public async Task EntitySave()
        {
            await Ctx.SaveChangesAsync();
        }


        #endregion


    }
}