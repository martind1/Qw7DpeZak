using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;

using QwTest7.Data;

namespace QwTest7
{
    public partial class QusyService
    {
        QusyContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly QusyContext context;
        private readonly NavigationManager navigationManager;

        public QusyService(QusyContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);


        public async Task ExportFilterabfragensToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/qusy/filterabfragens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/qusy/filterabfragens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportFilterabfragensToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/qusy/filterabfragens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/qusy/filterabfragens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnFilterabfragensRead(ref IQueryable<QwTest7.Models.Qusy.Filterabfragen> items);

        public async Task<IQueryable<QwTest7.Models.Qusy.Filterabfragen>> GetFilterabfragens(Query query = null)
        {
            var items = Context.Filterabfragens.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
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

            OnFilterabfragensRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnFilterabfragenGet(QwTest7.Models.Qusy.Filterabfragen item);

        public async Task<QwTest7.Models.Qusy.Filterabfragen> GetFilterabfragenByFltrid(int fltrid)
        {
            var items = Context.Filterabfragens
                              .AsNoTracking()
                              .Where(i => i.FLTRID == fltrid);

  
            var itemToReturn = items.FirstOrDefault();

            OnFilterabfragenGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnFilterabfragenCreated(QwTest7.Models.Qusy.Filterabfragen item);
        partial void OnAfterFilterabfragenCreated(QwTest7.Models.Qusy.Filterabfragen item);

        public async Task<QwTest7.Models.Qusy.Filterabfragen> CreateFilterabfragen(QwTest7.Models.Qusy.Filterabfragen filterabfragen)
        {
            OnFilterabfragenCreated(filterabfragen);

            var existingItem = Context.Filterabfragens
                              .Where(i => i.FLTRID == filterabfragen.FLTRID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Filterabfragens.Add(filterabfragen);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(filterabfragen).State = EntityState.Detached;
                throw;
            }

            OnAfterFilterabfragenCreated(filterabfragen);

            return filterabfragen;
        }

        public async Task<QwTest7.Models.Qusy.Filterabfragen> CancelFilterabfragenChanges(QwTest7.Models.Qusy.Filterabfragen item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnFilterabfragenUpdated(QwTest7.Models.Qusy.Filterabfragen item);
        partial void OnAfterFilterabfragenUpdated(QwTest7.Models.Qusy.Filterabfragen item);

        public async Task<QwTest7.Models.Qusy.Filterabfragen> UpdateFilterabfragen(int fltrid, QwTest7.Models.Qusy.Filterabfragen filterabfragen)
        {
            OnFilterabfragenUpdated(filterabfragen);

            var itemToUpdate = Context.Filterabfragens
                              .Where(i => i.FLTRID == filterabfragen.FLTRID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(filterabfragen);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterFilterabfragenUpdated(filterabfragen);

            return filterabfragen;
        }

        partial void OnFilterabfragenDeleted(QwTest7.Models.Qusy.Filterabfragen item);
        partial void OnAfterFilterabfragenDeleted(QwTest7.Models.Qusy.Filterabfragen item);

        public async Task<QwTest7.Models.Qusy.Filterabfragen> DeleteFilterabfragen(int fltrid)
        {
            var itemToDelete = Context.Filterabfragens
                              .Where(i => i.FLTRID == fltrid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnFilterabfragenDeleted(itemToDelete);


            Context.Filterabfragens.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterFilterabfragenDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}