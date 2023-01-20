using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using System.Text.Json;


namespace QwTest7.Services.Kmp
{
    public enum PageMode
    {
        Multi, Single
    }

    public enum KommandoTyp
    {
        Suchen, Refresh, Pagesize, Erfass, Edit
    }

    public enum DataState
    {
        Inactive, Browse, Edit, Insert, Query
    }

    /// <summary>
    /// Service für globale Navigation
    /// </summary>
    public class GlobalService : ComponentBase
    {
        public GlobalService() 
        { 
            ActivePage = new PageDescription();
        }

        public event Action<KommandoTyp> OnDoKommando;
        private void DoKommando(KommandoTyp Kommando) => OnDoKommando?.Invoke(Kommando);
        public void Kommando(KommandoTyp KTyp)
        {
            DoKommando(KTyp);
        }

        public int DefaultPageSize { get; set; }  //Vorgabe für grid.PageSize
        public bool EventConsoleVisible { get; set; } = true;
        public PageDescription ActivePage { get; set; }

        public event Action OnGnavChange;
        public void GnavChanged() => OnGnavChange?.Invoke();  //Page, Eventconsole geändert. Für GlobalNavigator

        //für grid.PageSizeOptions
        public IEnumerable<int> PageSizeValues { get; set; } = new int[] 
        { 
            10, 
            15,
            20,
            25,
            30,
            100,
            500,
            5000,
            9999999
        };

        private int _maxRecordCount = 50;
        public int MaxRecordCount
        {
            get => _maxRecordCount;
            set { _maxRecordCount = value; Kommando(KommandoTyp.Pagesize); }
        }


        #region Routing
        //Idee: navLink. Ziel: in Page-File: Navlink nl = new NavLink('SPED'), Data von JSON-DB/R_INIT



        #endregion


    }
    public class PageDescription
    {
        public string Page { get; set; }  //Kürzel der aktiven Seite zB 'FRZG'
        public string Title { get; set; }  //Titel der aktiven Seite zB 'Fahrzeuge'
        public string Abfrage { get; set; }  //aktuelle Abfrage der Seite (Standard)
        public Object PrimaryKey { get; set; } //string oder string[] wenn mehrere
        public PageMode pageMode { get; set; }
        public string FromPage { get; set; }  //LookUp
        public string FromAbfrage { get; set; }  //LookUp
    }
}