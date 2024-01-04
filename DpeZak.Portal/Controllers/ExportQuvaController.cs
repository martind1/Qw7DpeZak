using DpeZak.Services.Db;
using Microsoft.AspNetCore.Mvc;

namespace DpeZak.Portal.Controllers;

public partial class ExportQuvaController(DpeDbService service) : ExportController
{
    private readonly DpeDbService service = service;

    [HttpGet("/export/Quva/fahrzeuges/csv")]
    [HttpGet("/export/Quva/fahrzeuges/csv(fileName='{fileName}')")]
    public async Task<FileStreamResult> ExportFahrzeugesToCSV(string fileName = null)
    {
        return ToCSV(ApplyQuery(await service.GetFahrzeuge(), Request.Query), fileName);
    }

    [HttpGet("/export/Quva/fahrzeuges/excel")]
    [HttpGet("/export/Quva/fahrzeuges/excel(fileName='{fileName}')")]
    public async Task<FileStreamResult> ExportFahrzeugesToExcel(string fileName = null)
    {
        return ToExcel(ApplyQuery(await service.GetFahrzeuge(), Request.Query), fileName);
    }

    [HttpGet("/export/Quva/kartens/csv")]
    [HttpGet("/export/Quva/kartens/csv(fileName='{fileName}')")]
    public async Task<FileStreamResult> ExportKartensToCSV(string fileName = null)
    {
        return ToCSV(ApplyQuery(await service.GetKarten(), Request.Query), fileName);
    }

    [HttpGet("/export/Quva/kartens/excel")]
    [HttpGet("/export/Quva/kartens/excel(fileName='{fileName}')")]
    public async Task<FileStreamResult> ExportKartensToExcel(string fileName = null)
    {
        return ToExcel(ApplyQuery(await service.GetKarten(), Request.Query), fileName);
    }

    [HttpGet("/export/Quva/speditionens/csv")]
    [HttpGet("/export/Quva/speditionens/csv(fileName='{fileName}')")]
    public async Task<FileStreamResult> ExportSpeditionensToCSV(string fileName = null)
    {
        return ToCSV(ApplyQuery(await service.GetSpeditionen(), Request.Query), fileName);
    }

    [HttpGet("/export/Quva/speditionens/excel")]
    [HttpGet("/export/Quva/speditionens/excel(fileName='{fileName}')")]
    public async Task<FileStreamResult> ExportSpeditionensToExcel(string fileName = null)
    {
        return ToExcel(ApplyQuery(await service.GetSpeditionen(), Request.Query), fileName);
    }
}
