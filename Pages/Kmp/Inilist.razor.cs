using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using QwTest7.Models.KmpDb;
using QwTest7.Services.Kmp;
using Radzen;
using Radzen.Blazor;
using System.Security.Cryptography;

namespace QwTest7.Pages.Kmp;

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

    protected IList<INITIALISIERUNGEN> iniSet { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        iniSet = Ini.SectionTypSet();
        AnweSet = Ini.AnweSet();
    }

    #region MD Grid decoration

    public IEnumerable<int> pageSizeOptions { get; set; } = new int[] { 10, 15, 20, 100, 5000 };
    public bool showPagerSummary = true;
    public string pagingSummaryFormat = "Seite {0} von {1} ({2} Datensätze)";
    public Density Density = Density.Compact;

    #endregion

    #region Tabs
    TabPosition tabPosition = TabPosition.Top;
    RadzenDataGrid<INITIALISIERUNGEN> anweGrid;
    bool? allGroupsExpanded = false;
    IList<INITIALISIERUNGEN> selectedList;
    IList<INITIALISIERUNGEN> AnweSet;

    void OnGroup(DataGridColumnGroupEventArgs<INITIALISIERUNGEN> args)
    {
        allGroupsExpanded = false;
    }

    #endregion


}
