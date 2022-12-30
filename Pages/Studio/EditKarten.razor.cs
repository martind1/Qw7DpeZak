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
    public partial class EditKarten
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

        [Parameter]
        public int KARTID { get; set; }

        protected override async Task OnInitializedAsync()
        {
            karten = await QuvaService.GetKartenByKartid(KARTID);

            fahrzeugesForFRZGID = await QuvaService.GetFahrzeuges();

            speditionensForSPEDID = await QuvaService.GetSpeditionens();
        }
        protected bool errorVisible;
        protected QwTest7.Models.Quva.Karten karten;

        protected IEnumerable<QwTest7.Models.Quva.Fahrzeuge> fahrzeugesForFRZGID;

        protected IEnumerable<QwTest7.Models.Quva.Speditionen> speditionensForSPEDID;

        protected async Task FormSubmit()
        {
            try
            {
                await QuvaService.UpdateKarten(KARTID, karten);
                DialogService.Close(karten);
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


        protected async Task ReloadButtonClick(MouseEventArgs args)
        {
           QuvaService.Reset();
            hasChanges = false;
            canEdit = true;

            karten = await QuvaService.GetKartenByKartid(KARTID);
        }
    }
}