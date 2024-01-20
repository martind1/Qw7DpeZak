using DpeZak.Database.Models;
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


}