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
    public partial class Filterabfragens
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
        public QusyService QusyService { get; set; }

        protected IEnumerable<Models.Qusy.Filterabfragen> filterabfragens;

        protected RadzenDataGrid<Models.Qusy.Filterabfragen> grid0;
        protected override async Task OnInitializedAsync()
        {
            filterabfragens = await QusyService.GetFilterabfragens();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await grid0.InsertRow(new Models.Qusy.Filterabfragen());
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, Models.Qusy.Filterabfragen filterabfragen)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await QusyService.DeleteFilterabfragen(filterabfragen.FLTRID);

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
                    Detail = $"Unable to delete Filterabfragen"
                });
            }
        }

        protected async Task GridRowUpdate(Models.Qusy.Filterabfragen args)
        {
            await QusyService.UpdateFilterabfragen(args.FLTRID, args);
        }

        protected async Task GridRowCreate(Models.Qusy.Filterabfragen args)
        {
            await QusyService.CreateFilterabfragen(args);
            await grid0.Reload();
        }

        protected async Task EditButtonClick(MouseEventArgs args, Models.Qusy.Filterabfragen data)
        {
            await grid0.EditRow(data);
        }

        protected async Task SaveButtonClick(MouseEventArgs args, Models.Qusy.Filterabfragen data)
        {
            await grid0.UpdateRow(data);
        }

        protected async Task CancelButtonClick(MouseEventArgs args, Models.Qusy.Filterabfragen data)
        {
            grid0.CancelEditRow(data);
            await QusyService.CancelFilterabfragenChanges(data);
        }
    }
}