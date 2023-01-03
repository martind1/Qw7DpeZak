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
    public partial class QuvaService
    {
        QuvaContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly QuvaContext context;
        private readonly NavigationManager navigationManager;

        public QuvaService(QuvaContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);


        partial void OnFahrzeugesRead(ref IQueryable<QwTest7.Models.Quva.Fahrzeuge> items);

        public async Task<IQueryable<QwTest7.Models.Quva.Fahrzeuge>> GetFahrzeuges(Query query = null)
        {
            var items = Context.Fahrzeuges.AsQueryable();

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

            OnFahrzeugesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnFahrzeugeGet(QwTest7.Models.Quva.Fahrzeuge item);

        public async Task<QwTest7.Models.Quva.Fahrzeuge> GetFahrzeugeByFrzgid(int frzgid)
        {
            var items = Context.Fahrzeuges
                              .AsNoTracking()
                              .Where(i => i.FRZGID == frzgid);

                items = items.Include(i => i.Speditionen);
  
            var itemToReturn = items.FirstOrDefault();

            OnFahrzeugeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnFahrzeugeCreated(QwTest7.Models.Quva.Fahrzeuge item);
        partial void OnAfterFahrzeugeCreated(QwTest7.Models.Quva.Fahrzeuge item);

        public async Task<QwTest7.Models.Quva.Fahrzeuge> CreateFahrzeuge(QwTest7.Models.Quva.Fahrzeuge fahrzeuge)
        {
            OnFahrzeugeCreated(fahrzeuge);

            var existingItem = Context.Fahrzeuges
                              .Where(i => i.FRZGID == fahrzeuge.FRZGID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Fahrzeuges.Add(fahrzeuge);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(fahrzeuge).State = EntityState.Detached;
                throw;
            }

            OnAfterFahrzeugeCreated(fahrzeuge);

            return fahrzeuge;
        }

        public async Task<QwTest7.Models.Quva.Fahrzeuge> CancelFahrzeugeChanges(QwTest7.Models.Quva.Fahrzeuge item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnFahrzeugeUpdated(QwTest7.Models.Quva.Fahrzeuge item);
        partial void OnAfterFahrzeugeUpdated(QwTest7.Models.Quva.Fahrzeuge item);

        public async Task<QwTest7.Models.Quva.Fahrzeuge> UpdateFahrzeuge(int frzgid, QwTest7.Models.Quva.Fahrzeuge fahrzeuge)
        {
            OnFahrzeugeUpdated(fahrzeuge);

            var itemToUpdate = Context.Fahrzeuges
                              .Where(i => i.FRZGID == fahrzeuge.FRZGID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(fahrzeuge);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterFahrzeugeUpdated(fahrzeuge);

            return fahrzeuge;
        }

        partial void OnFahrzeugeDeleted(QwTest7.Models.Quva.Fahrzeuge item);
        partial void OnAfterFahrzeugeDeleted(QwTest7.Models.Quva.Fahrzeuge item);

        public async Task<QwTest7.Models.Quva.Fahrzeuge> DeleteFahrzeuge(int frzgid)
        {
            var itemToDelete = Context.Fahrzeuges
                              .Where(i => i.FRZGID == frzgid)
                              .Include(i => i.Kartens)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnFahrzeugeDeleted(itemToDelete);


            Context.Fahrzeuges.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterFahrzeugeDeleted(itemToDelete);

            return itemToDelete;
        }
    
        partial void OnKartensRead(ref IQueryable<QwTest7.Models.Quva.Karten> items);

        public async Task<IQueryable<QwTest7.Models.Quva.Karten>> GetKartens(Query query = null)
        {
            var items = Context.Kartens.AsQueryable();

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

            OnKartensRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnKartenGet(QwTest7.Models.Quva.Karten item);

        public async Task<QwTest7.Models.Quva.Karten> GetKartenByKartid(int kartid)
        {
            var items = Context.Kartens
                              .AsNoTracking()
                              .Where(i => i.KARTID == kartid);

                items = items.Include(i => i.Fahrzeuge);
                items = items.Include(i => i.Speditionen);
  
            var itemToReturn = items.FirstOrDefault();

            OnKartenGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnKartenCreated(QwTest7.Models.Quva.Karten item);
        partial void OnAfterKartenCreated(QwTest7.Models.Quva.Karten item);

        public async Task<QwTest7.Models.Quva.Karten> CreateKarten(QwTest7.Models.Quva.Karten karten)
        {
            OnKartenCreated(karten);

            var existingItem = Context.Kartens
                              .Where(i => i.KARTID == karten.KARTID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Kartens.Add(karten);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(karten).State = EntityState.Detached;
                throw;
            }

            OnAfterKartenCreated(karten);

            return karten;
        }

        public async Task<QwTest7.Models.Quva.Karten> CancelKartenChanges(QwTest7.Models.Quva.Karten item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnKartenUpdated(QwTest7.Models.Quva.Karten item);
        partial void OnAfterKartenUpdated(QwTest7.Models.Quva.Karten item);

        public async Task<QwTest7.Models.Quva.Karten> UpdateKarten(int kartid, QwTest7.Models.Quva.Karten karten)
        {
            OnKartenUpdated(karten);

            var itemToUpdate = Context.Kartens
                              .Where(i => i.KARTID == karten.KARTID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(karten);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterKartenUpdated(karten);

            return karten;
        }

        partial void OnKartenDeleted(QwTest7.Models.Quva.Karten item);
        partial void OnAfterKartenDeleted(QwTest7.Models.Quva.Karten item);

        public async Task<QwTest7.Models.Quva.Karten> DeleteKarten(int kartid)
        {
            var itemToDelete = Context.Kartens
                              .Where(i => i.KARTID == kartid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnKartenDeleted(itemToDelete);


            Context.Kartens.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterKartenDeleted(itemToDelete);

            return itemToDelete;
        }
    
        partial void OnSpeditionensRead(ref IQueryable<QwTest7.Models.Quva.Speditionen> items);

        public async Task<IQueryable<QwTest7.Models.Quva.Speditionen>> GetSpeditionens(Query query = null)
        {
            var items = Context.Speditionens.AsQueryable();

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

            OnSpeditionensRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSpeditionenGet(QwTest7.Models.Quva.Speditionen item);

        public async Task<QwTest7.Models.Quva.Speditionen> GetSpeditionenBySpedid(int spedid)
        {
            var items = Context.Speditionens
                              .AsNoTracking()
                              .Where(i => i.SPEDID == spedid);

  
            var itemToReturn = items.FirstOrDefault();

            OnSpeditionenGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSpeditionenCreated(QwTest7.Models.Quva.Speditionen item);
        partial void OnAfterSpeditionenCreated(QwTest7.Models.Quva.Speditionen item);

        public async Task<QwTest7.Models.Quva.Speditionen> CreateSpeditionen(QwTest7.Models.Quva.Speditionen speditionen)
        {
            OnSpeditionenCreated(speditionen);

            var existingItem = Context.Speditionens
                              .Where(i => i.SPEDID == speditionen.SPEDID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Speditionens.Add(speditionen);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(speditionen).State = EntityState.Detached;
                throw;
            }

            OnAfterSpeditionenCreated(speditionen);

            return speditionen;
        }

        public async Task<QwTest7.Models.Quva.Speditionen> CancelSpeditionenChanges(QwTest7.Models.Quva.Speditionen item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSpeditionenUpdated(QwTest7.Models.Quva.Speditionen item);
        partial void OnAfterSpeditionenUpdated(QwTest7.Models.Quva.Speditionen item);

        public async Task<QwTest7.Models.Quva.Speditionen> UpdateSpeditionen(int spedid, QwTest7.Models.Quva.Speditionen speditionen)
        {
            OnSpeditionenUpdated(speditionen);

            var itemToUpdate = Context.Speditionens
                              .Where(i => i.SPEDID == speditionen.SPEDID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(speditionen);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSpeditionenUpdated(speditionen);

            return speditionen;
        }

        partial void OnSpeditionenDeleted(QwTest7.Models.Quva.Speditionen item);
        partial void OnAfterSpeditionenDeleted(QwTest7.Models.Quva.Speditionen item);

        public async Task<QwTest7.Models.Quva.Speditionen> DeleteSpeditionen(int spedid)
        {
            var itemToDelete = Context.Speditionens
                              .Where(i => i.SPEDID == spedid)
                              .Include(i => i.Fahrzeuges)
                              .Include(i => i.Kartens)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSpeditionenDeleted(itemToDelete);


            Context.Speditionens.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSpeditionenDeleted(itemToDelete);

            return itemToDelete;
        }
    }
}