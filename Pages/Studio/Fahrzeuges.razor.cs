using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace QwTest7.Pages.Studio
{
    public partial class Fahrzeuges
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public QuvaService QuvaService { get; set; }

        protected IEnumerable<Models.Quva.Fahrzeuge> fahrzeuges;

        protected RadzenDataGrid<Models.Quva.Fahrzeuge> grid0;

        [Inject]
        protected SecurityService Security { get; set; }
        protected override async Task OnInitializedAsync()
        {
            fahrzeuges = await QuvaService.GetFahrzeuges(new Query { Expand = "Speditionen" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddFahrzeuge>("Add Fahrzeuge", null);
            await grid0.Reload();
        }

        protected async Task EditRow(Models.Quva.Fahrzeuge args)
        {
            await DialogService.OpenAsync<EditFahrzeuge>("Edit Fahrzeuge", new Dictionary<string, object> { { "FRZGID", args.FRZGID } });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, Models.Quva.Fahrzeuge fahrzeuge)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await QuvaService.DeleteFahrzeuge(fahrzeuge.FRZGID);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete Fahrzeuge"
                });
            }
        }
        #region Grid decoration
        public IEnumerable<int> pageSizeOptions { get; set; } = new int[] { 10, 20, 30 };
        public bool showPagerSummary = true;
        public string pagingSummaryFormat = "Seite {0} von {1} ({2} Datensätze)";
        public Density Density = Density.Compact;
        #endregion
    }
}