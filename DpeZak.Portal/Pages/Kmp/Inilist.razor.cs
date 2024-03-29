using DpeZak.Database.Models;
using DpeZak.Services.Kmp;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;

namespace DpeZak.Portal.Pages.Kmp;

public partial class Inilist
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
    GlobalService Gnav { get; set; }
    [Inject]
    IniDbService Ini { get; set; }

    protected IList<RInit> iniSet { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        iniSet = Ini.SectionTypSet();
        AnweSet = Ini.AnweSet();
        MaschineSet = Ini.MaschineSet();
        UserSet = Ini.UserSet();
        VorgabeSet = Ini.VorgabeSet();
        WerkparameterSet = await Ini.SectionParameterSet("QUVA", "PRGPARAM");
    }

    #region MD Grid decoration

    public IEnumerable<int> pageSizeOptions { get; set; } = new int[] { 10, 15, 20, 100, 5000 };
    public bool showPagerSummary = true;
    public string pagingSummaryFormat = "Seite {0} von {1} ({2} Datensätze)";
    public Density Density = Density.Compact;

    #endregion

    #region Tabs
    private readonly TabPosition tabPosition = TabPosition.Top;
    bool? allGroupsExpanded = false;
    private RadzenDataGrid<RInit> anweGrid;
    private RadzenDataGrid<RInit> maschineGrid;
    private RadzenDataGrid<RInit> userGrid;
    private RadzenDataGrid<RInit> vorgabeGrid;
    private RadzenDataGrid<RInit> werkparameterGrid;

    IList<RInit> selectedList;
    IList<RInit> AnweSet;
    IList<RInit> MaschineSet;
    IList<RInit> UserSet;
    IList<RInit> VorgabeSet;
    IList<RInit> WerkparameterSet;

    void OnGroup(DataGridColumnGroupEventArgs<RInit> args)
    {
        allGroupsExpanded = false;
    }

    #endregion


}
