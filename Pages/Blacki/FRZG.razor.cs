using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
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
using QwTest7.Data;
using Serilog;
using Serilog.Context;

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
    protected SecurityService Security { get; set; }

    [Inject]
    public BlackiService BlackiService { get; set; }
    [Inject]
    protected GlobalService Gnav { get; set; }
    [Inject]
    public ProtService Prot { get; set; }
    [Inject]
    public IniDbService Ini { get; set; }

    protected IEnumerable<FAHRZEUGE> tbl0;
    protected RadzenDataGrid<FAHRZEUGE> grid0;

    protected override async Task OnInitializedAsync()
    {
        //string clientIp = LogContext
        //weg wg LoadData - tbl0 = await BlackiService.EntityGetAsync<FAHRZEUGE>(new Query { Expand = "SPED" });
        Log.Information($"### OnInitializedAsync User({Gnav.UserName}) IP({Gnav.IPAddress}) Maschine({Gnav.MaschineName})");
        //Test:
        StatusInit();
    }

    protected async Task AddButtonClick(MouseEventArgs args)
    {
        //await DialogService.OpenAsync<Studio.AddFahrzeuge>("Add Fahrzeuge", null);
        //await grid0.Reload();
        //Test:
        //await JSRuntime.InvokeAsync<object>("open", "speditionens", "_blank");
        Prot.Prot0SL("Fire");
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
    #region MD Prot Test
    public string StatusLine { get; set; }

    public void StatusInit()
    {
        Prot.OnStatusListAdd += StatusListAdd1;
        Prot.OnStatusListAdd += StatusListAdd2;
    }

    public void StatusListAdd1(StatusListEntry statusListEntry)
    {
        StatusLine += $" Add1[{statusListEntry.Date:mm:ss}]:{statusListEntry.Text}";
    }

    public void StatusListAdd2(StatusListEntry statusListEntry)
    {
        StatusLine += $" Add2[{statusListEntry.Date:mm:ss}]:{statusListEntry.Text}";
    }
    #endregion

    #region LoadData

    public int RecordCount = 0;
    public bool isLoading = false;

    async Task LoadData(LoadDataArgs args)
    {
        isLoading = true;

        Log.Information($"### LoadData: {args.Skip.Value} bis {args.Top.Value}");

        await Task.Yield();

        // This demo is using https://dynamic-linq.net
        var query = BlackiService.QueryFromLoadDataArgs(args);
        query.Expand = "SPED";

        // Perform paginv via Skip and Take.
        tbl0 = await BlackiService.EntityGetAsync<FAHRZEUGE>(query);
        Log.Information($"### LoadData: loaded");

        // Important!!! Make sure the Count property of RadzenDataGrid is set.
        RecordCount = await BlackiService.EntityQueryCountAsync<FAHRZEUGE>(query);
        Log.Information($"### LoadData: counted");

        isLoading = false;
    }

    #endregion

    #region MD Grid decoration

    public IEnumerable<int> pageSizeOptions { get; set; } = new int[] { 10, 20, 30 };
    public bool showPagerSummary = true;
    public string pagingSummaryFormat = "Seite {0} von {1} ({2} Datensätze)";
    public Density Density = Density.Compact;

    IList<FAHRZEUGE> selectedList;

    #endregion

    #region Test Ini

    private string PageName = "FRZG";
    public int PageSize { get; set; } = 15;

    public void IniInit()
    {
        PageSize = Ini.ReadItem("PAGESTATUS." + PageName, "PAGESIZE", 14);
    }

    public void SavePageSize(int pagesize)
    {
        Ini.WriteItem("PAGESTATUS." + PageName, "PAGESIZE", pagesize);
    }


    #endregion
}