using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Serilog;
using System.Net;
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

    //so nicht - public interface IGlobalService
    //{
    //    string GetConstructorParameter();
    //}
    
    /// <summary>
         /// Service für globale Navigation
         /// </summary>
    public class GlobalService : ComponentBase
    {
        //so nicht. Is null! [Inject] protected SecurityService Security { get; set; }
        //public string UserName()
        //{
        //    string username = String.Empty;
        //    try
        //    {
        //        username = Security.User.ToString();
        //    }
        //    catch
        //    {
        //    }
        //    return string.IsNullOrEmpty(username) ? "anonymous" : username; //vergl program.cs
        //}

        public GlobalService()
        {
            ActivePage = new PageDescription();
            AnweKennung = BaseUtils.ReadSetting("AnweKennung", "NoAnwe");  //QUVAR3
            IniAnwe = BaseUtils.ReadSetting("IniAnwe", "NoIniAnwe");  //QUVAE
            anweLogged = false;
        }

        //so nicht - public GlobalService(string anweKennung)
        //{
        //    ActivePage = new PageDescription();
        //    AnweKennung= anweKennung;
        //}
        //public string GetConstructorParameter()
        //{
        //    return anweKennung;
        //}

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

        #region User Infos
        public event Action OnAnweChanged;
        private void AnweChanged() => OnAnweChanged?.Invoke();
        public event Action OnIniAnweChanged;
        private void IniAnweChanged() => OnIniAnweChanged?.Invoke();
        public event Action OnMaschineChanged;
        private void MaschineChanged() => OnMaschineChanged?.Invoke();
        public event Action OnUserChanged;
        private void UserChanged() => OnUserChanged?.Invoke();

        private string anweKennung;
        private string iniAnwe;
        private bool anweLogged = false;
        private string userAgent;
        private string maschineName;
        private string userName = "anonymous";
        private string iPAddress;

        /// <summary>
        /// die Werte werden im Constructor von appsettings.json bestimmt
        /// </summary>
        public string AnweKennung
        {
            get => anweKennung;
            set
            {
                if (anweKennung != value)
                {
                    Log.Information($"### AnweKennung({value})<-({anweKennung})");
                    AnweChanged();  //Ereignis für Ini
                }
                anweLogged = true;
                anweKennung = value;
            }
        }
        public string IniAnwe
        {
            get => iniAnwe;
            set
            {
                if (iniAnwe != value)
                {
                    Log.Information($"### IniAnwe({value})<-({iniAnwe})");
                    IniAnweChanged();  //Ereignis für Ini
                }
                iniAnwe = value;
            }
        }
        /// <summary>
        /// die Werte werden in Index.razor.cs hierher geschrieben
        /// </summary>
        public string UserAgent
        {
            get => userAgent;
            set
            {
                if (userAgent != value)
                    Log.Information($"### UserAgent({value})<-({userAgent})");
                userAgent = value;
            }
        }
        public string MaschineName
        {
            get => maschineName;
            set
            {
                if (maschineName != value)
                {
                    Log.Information($"### MaschineName({value})<-({maschineName})");
                    MaschineChanged();  //Ereignis für Ini
                }
                maschineName = value;
            }
        }
        public string UserName 
        { 
            get => userName;
            set
            {
                if (userName != value)
                {
                    Log.Information($"### UserName({value})<-({userName})");
                    UserChanged();  //Ereignis für Ini
                }
                userName = value;
            }
        }
        public string IPAddress
        {
            get => iPAddress;
            set
            {
                if (!anweLogged)
                {
                    Log.Debug($"### AnweKennung({AnweKennung})");
                    AnweChanged();  //Ereignis für Ini
                }
                anweLogged = true;
                if (iPAddress != value)
                    Log.Information($"### IPAddress({value})<-({iPAddress})");
                iPAddress = value;
                MaschineName = GetMachineNameFromIPAddress(value);
            }
        }

        public static string GetMachineNameFromIPAddress(string ipAddress)
        {
            string machineName;
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAddress);

                machineName = hostEntry.HostName;
                machineName = machineName.Split('.')[0];  //blacki.sand.int -> blacki
            }
            catch (Exception ex)
            {
                Log.Warning($"GetMachineNameFromIPAddress({ipAddress})", ex);
                // Machine not found...
                machineName = ipAddress;
            }
            return machineName;
        }


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