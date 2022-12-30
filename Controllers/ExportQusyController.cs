using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using QwTest7.Data;

namespace QwTest7.Controllers
{
    public partial class ExportQusyController : ExportController
    {
        private readonly QusyContext context;
        private readonly QusyService service;

        public ExportQusyController(QusyContext context, QusyService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/Qusy/filterabfragens/csv")]
        [HttpGet("/export/Qusy/filterabfragens/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFilterabfragensToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetFilterabfragens(), Request.Query), fileName);
        }

        [HttpGet("/export/Qusy/filterabfragens/excel")]
        [HttpGet("/export/Qusy/filterabfragens/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFilterabfragensToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetFilterabfragens(), Request.Query), fileName);
        }
    }
}
