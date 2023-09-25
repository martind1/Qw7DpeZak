using Microsoft.AspNetCore.Mvc;

using QwTest7.Database.Models;
using QwTest7.Portal.Services;

namespace QwTest7.Portal.Controllers;

public partial class ExportQuvaController : ExportController
{
    private readonly QuvaDbService service;

    public ExportQuvaController(QuvaDbService service)
    {
        this.service = service;
    }

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
