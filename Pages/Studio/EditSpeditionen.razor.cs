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
    public partial class EditSpeditionen
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
        public int SPEDID { get; set; }

        protected override async Task OnInitializedAsync()
        {
            speditionen = await QuvaService.GetSpeditionenBySpedid(SPEDID);
        }
        protected bool errorVisible;
        protected QwTest7.Models.Quva.Speditionen speditionen;

        protected async Task FormSubmit()
        {
            try
            {
                await QuvaService.UpdateSpeditionen(SPEDID, speditionen);
                DialogService.Close(speditionen);
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

            speditionen = await QuvaService.GetSpeditionenBySpedid(SPEDID);
        }
    }
}