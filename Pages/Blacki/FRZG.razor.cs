using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Microsoft.AspNetCore.Mvc.Rendering;
using QwTest7.Models.Blacki;
using QwTest7.Services;
using QwTest7.Services.Kmp;

namespace QwTest7.Pages.Blacki;

public partial class FRZG
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
    public BlackiService BlackiService { get; set; }

    [Inject]
    protected SecurityService Security { get; set; }

    protected IEnumerable<FAHRZEUGE> tbl0;
    protected RadzenDataGrid<FAHRZEUGE> grid0;

    protected override async Task OnInitializedAsync()
    {
        tbl0 = await BlackiService.EntityGet<FAHRZEUGE>(new Query { Expand = "SPEDITIONEN" });
    }

    protected async Task AddButtonClick(MouseEventArgs args)
    {
        await DialogService.OpenAsync<Studio.AddFahrzeuge>("Add Fahrzeuge", null);
        await grid0.Reload();
    }

    protected async Task EditRow(FAHRZEUGE args)
    {
        await DialogService.OpenAsync<Studio.EditFahrzeuge>("Edit Fahrzeuge", new Dictionary<string, object> { { "FRZG_ID", args.FRZG_ID } });
    }

    protected async Task GridDeleteButtonClick(MouseEventArgs args, FAHRZEUGE entity)
    {
        try
        {
            if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
            {
                if (tbl0.Contains(entity))
                    throw new KmpException(String.Format("{0} nicht gefunden", entity.GetType().Name));

                var deleteResult = await BlackiService.EntityDelete<FAHRZEUGE>(entity);
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
                Detail = String.Format("Unable to delete {0}:{1}{2}", entity.GetType().Name, Environment.NewLine, ex.Message)
            });
        }
    }
    #region MD Grid decoration

    public IEnumerable<int> pageSizeOptions { get; set; } = new int[] { 10, 20, 30 };
    public bool showPagerSummary = true;
    public string pagingSummaryFormat = "Seite {0} von {1} ({2} Datensätze)";
    public Density Density = Density.Compact;

    IList<FAHRZEUGE> selectedList;

    #endregion
}