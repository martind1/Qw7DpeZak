using DpeZak.Database.Models;
using DpeZak.Services.Db;
using DpeZak.Services.Kmp;
using DpeZak.Services.Kmp.Exceptions;
using DpeZak.Services.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using Serilog;

namespace DpeZak.Portal.Pages.Blacki;

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
    public DpeDbService BlackiService { get; set; }
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
        IniInit();  //PageSize
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
        //await DialogService.OpenAsync<EditFahrzeuge>("Edit Fahrzeuge", new Dictionary<string, object> { { "FRZG_ID", args.FRZG_ID } });
    }

    protected async Task GridDeleteButtonClick(MouseEventArgs args, FAHRZEUGE entity)
    {
        try
        {
            if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
            {
                if (tbl0.Contains(entity))
                    throw new KmpException(string.Format("{0} nicht gefunden", entity.GetType().Name));

                var deleteResult = await BlackiService.EntityDelete(entity);
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
                Detail = string.Format("Unable to delete {0}:{1}{2}", entity.GetType().Name, Environment.NewLine, ex.Message)
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

        // await Task.Yield();  // why?

        // This demo is using https://dynamic-linq.net
        var query = BlackiService.QueryFromLoadDataArgs(args);
        query.Expand = "SPED";

        // Perform paging via Skip and Take.
        tbl0 = await BlackiService.EntityGet<FAHRZEUGE>(query).ToListAsync();
        Log.Information($"### LoadData: loaded");

        // Important!!! Make sure the Count property of RadzenDataGrid is set.
        RecordCount = await BlackiService.EntityQueryCount<FAHRZEUGE>(query);
        Log.Information($"### LoadData: counted");

        await SavePageSizeAsync(PageSize);

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

    private readonly string PageName = "FRZG";
    public int PageSize { get; set; } = 15;

    public void IniInit()
    {
        PageSize = Ini.ReadItem("PAGESTATUS." + PageName, "PAGESIZE", 14);
    }

    public async Task SavePageSizeAsync(int pagesize)
    {
        await Ini.WriteItem("PAGESTATUS." + PageName, "PAGESIZE", pagesize);
    }


    #endregion
}