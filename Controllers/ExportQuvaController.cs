using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using QwTest7.Data;

namespace QwTest7.Controllers
{
    public partial class ExportQuvaController : ExportController
    {
        private readonly QuvaContext context;
        private readonly QuvaService service;

        public ExportQuvaController(QuvaContext context, QuvaService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/Quva/fahrzeuges/csv")]
        [HttpGet("/export/Quva/fahrzeuges/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFahrzeugesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetFahrzeuges(), Request.Query), fileName);
        }

        [HttpGet("/export/Quva/fahrzeuges/excel")]
        [HttpGet("/export/Quva/fahrzeuges/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFahrzeugesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetFahrzeuges(), Request.Query), fileName);
        }

        [HttpGet("/export/Quva/kartens/csv")]
        [HttpGet("/export/Quva/kartens/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportKartensToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetKartens(), Request.Query), fileName);
        }

        [HttpGet("/export/Quva/kartens/excel")]
        [HttpGet("/export/Quva/kartens/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportKartensToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetKartens(), Request.Query), fileName);
        }

        [HttpGet("/export/Quva/speditionens/csv")]
        [HttpGet("/export/Quva/speditionens/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSpeditionensToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSpeditionens(), Request.Query), fileName);
        }

        [HttpGet("/export/Quva/speditionens/excel")]
        [HttpGet("/export/Quva/speditionens/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSpeditionensToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSpeditionens(), Request.Query), fileName);
        }
    }
}
