﻿using DpeZak.Database.Models;
using Radzen;

namespace DpeZak.Services.Db;

/// <summary>
/// Datenservice für QUVA
/// </summary>
public partial class DpeDbService : BaseDbService
{
    public DpeDbService(DpeContext ctx) : base(ctx)
    {
    }

    public DpeContext AppCtx()
    {
        return (DpeContext)Ctx;
    }

    public async Task<IQueryable<FAHRZEUGE>> GetFahrzeuge(Query query = null)
    {
        var items = EntityGet<FAHRZEUGE>(query);
        return await Task.FromResult(items);

    }

    public async Task<IQueryable<KARTEN>> GetKarten(Query query = null)
    {
        var items = EntityGet<KARTEN>(query);
        return await Task.FromResult(items);

    }

    public async Task<IQueryable<SPEDITIONEN>> GetSpeditionen(Query query = null)
    {
        var items = EntityGet<SPEDITIONEN>(query);
        return await Task.FromResult(items);

    }

}