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

    protected IList<R_INIT> iniSet { get; set; }

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
    private RadzenDataGrid<R_INIT> anweGrid;
    private RadzenDataGrid<R_INIT> maschineGrid;
    private RadzenDataGrid<R_INIT> userGrid;
    private RadzenDataGrid<R_INIT> vorgabeGrid;
    private RadzenDataGrid<R_INIT> werkparameterGrid;

    IList<R_INIT> selectedList;
    IList<R_INIT> AnweSet;
    IList<R_INIT> MaschineSet;
    IList<R_INIT> UserSet;
    IList<R_INIT> VorgabeSet;
    IList<R_INIT> WerkparameterSet;

    void OnGroup(DataGridColumnGroupEventArgs<R_INIT> args)
    {
        allGroupsExpanded = false;
    }

    #endregion


}
