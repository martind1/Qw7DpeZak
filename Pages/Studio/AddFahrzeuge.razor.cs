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
    public partial class AddFahrzeuge
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

        protected override async Task OnInitializedAsync()
        {
            fahrzeuge = new Models.Quva.Fahrzeuge();

            speditionensForSPEDID = await QuvaService.GetSpeditionens();
        }
        protected bool errorVisible;
        protected Models.Quva.Fahrzeuge fahrzeuge;

        protected IEnumerable<Models.Quva.Speditionen> speditionensForSPEDID;

        protected async Task FormSubmit()
        {
            try
            {
                await QuvaService.CreateFahrzeuge(fahrzeuge);
                DialogService.Close(fahrzeuge);
            }
            catch (Exception ex)
            {
                hasChanges = ex is Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException;
                canEdit = !(ex is Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException);
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }


        protected bool hasChanges = false;
        protected bool canEdit = true;

        [Inject]
        protected SecurityService Security { get; set; }
    }
}