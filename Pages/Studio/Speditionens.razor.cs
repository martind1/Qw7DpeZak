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
    public partial class Speditionens
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

        protected IEnumerable<QwTest7.Models.Quva.Speditionen> speditionens;

        protected RadzenDataGrid<QwTest7.Models.Quva.Speditionen> grid0;

        [Inject]
        protected SecurityService Security { get; set; }
        protected override async Task OnInitializedAsync()
        {
            speditionens = await QuvaService.GetSpeditionens();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddSpeditionen>("Add Speditionen", null);
            await grid0.Reload();
        }

        protected async Task EditRow(QwTest7.Models.Quva.Speditionen args)
        {
            await DialogService.OpenAsync<EditSpeditionen>("Edit Speditionen", new Dictionary<string, object> { {"SPEDID", args.SPEDID} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, QwTest7.Models.Quva.Speditionen speditionen)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await QuvaService.DeleteSpeditionen(speditionen.SPEDID);

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
                    Detail = $"Unable to delete Speditionen" 
                });
            }
        }
    }
}