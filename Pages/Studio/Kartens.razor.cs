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
    public partial class Kartens
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

        protected IEnumerable<QwTest7.Models.Quva.Karten> kartens;

        protected RadzenDataGrid<QwTest7.Models.Quva.Karten> grid0;

        [Inject]
        protected SecurityService Security { get; set; }
        protected override async Task OnInitializedAsync()
        {
            kartens = await QuvaService.GetKartens(new Query { Expand = "Fahrzeuge,Speditionen" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddKarten>("Add Karten", null);
            await grid0.Reload();
        }

        protected async Task EditRow(QwTest7.Models.Quva.Karten args)
        {
            await DialogService.OpenAsync<EditKarten>("Edit Karten", new Dictionary<string, object> { {"KARTID", args.KARTID} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, QwTest7.Models.Quva.Karten karten)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await QuvaService.DeleteKarten(karten.KARTID);

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
                    Detail = $"Unable to delete Karten" 
                });
            }
        }
    }
}