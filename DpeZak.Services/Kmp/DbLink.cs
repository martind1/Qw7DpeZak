using DpeZak.Database.Models;
using Microsoft.AspNetCore.Components;
using static System.StringComparison;
using DpeZak.Services.Kmp;
using Radzen;
using System.Text.Json;
using DpeZak.Services.Kmp.Helper;
using DpeZak.Services.Kmp.Enums;

namespace DpeZak.Portal.Services.Kmp
{
    public interface IDbLink
    {
        public int Recordcount { get; set; }
        public string Pagetitle { get; set; }
    }

    public class DummyEntity
    {
        public Guid Id { get; set; }
    }


    /// <summary>
    /// Basisklasse für LocalService und LookupDef: gemeinsame Sachen
    /// </summary>
    public partial class DbLink<TItem> where TItem : class
    {
        [Inject]
        private GlobalService Gnav { get; set; }
        [Inject]
        private ProtService Prot { get; set; }
        [Inject]
        private KmpDbService KmpDbService { get; set; }

        //für SQL/Linq Generierung:
        public ColumnList Columnlist { get; set; }
        public string KeyFields { get; set; }
        public string OrderBy { get; set; }  //von Radzen.LoadDataArgs
        public FltrList Fltrlist { get; set; }
        public FltrList References { get; set; }  //nicht durch User änderbar
        public IDictionary<string, FieldInfo> EntityFieldlist { get; set; } //Liste mit Original Feldnamen (Groß/Kleinschreibung)
        public string Page { get; private set; }

        public string Abfrage { get => abfrage; set => abfrage = LoadAbfrage(value); }
        public FLTR FltrRec { get => fltrRec; set => fltrRec = value; }

        //Steuerung:
        private FLTR fltrRec;
        private string abfrage;

        public DbLink(string page, string abfrage)
        {
            Page = page;
            Abfrage = abfrage;  //LoadAbfrage

        }

        // Record mit Columnlist, Keyfields, Fltrlist von Table FLTR[FormKurz, Abfrage] laden
        public string LoadAbfrage(string value)
        {
            return LoadAbfrageAsync(value).GetAwaiter().GetResult();
        }

        public async Task<string> LoadAbfrageAsync(string value)
        {
            abfrage = value;
            FltrRec = await KmpDbService.GetFltr(Page, Abfrage);  //null wenn nicht vorhanden ist auch gut
            Columnlist = LoadColumnlist();
            KeyFields = LoadKeyFields();  //erst hier wg ergänzt Columnlist
            Fltrlist = LoadFltrlist();
            return value;
        }

        public ColumnList LoadColumnlist()
        {
            if (FltrRec.COLUMNLIST is null)
                throw new ArgumentNullException(nameof(FltrRec.COLUMNLIST));
            //Test: statische Liste
            //todo: von Abfrage laden
            //string cl = 
            //@"Quittung:5=HOFL_KTRL
            //sta:0=sta
            //edt:0=edt
            //Lieferart:10=lityp
            //Ein:5=ETm
            //Fahrzeug:11=fahr_knz
            //Beförderer:23=anl_na1
            //Sorte Bez.:21=srte_bez
            //Tara:8=tagew
            //erz_na1:13=erz_na1
            //erz_na2:14=erz_na2
            //erz_str:15=erz_str";
            //return new ColumnList(cl);

            ColumnList columnlist = new(FltrRec.COLUMNLIST);  // DB when exists
            int width = FltrRec == null ? 8 : 0;

            //Groß/Klein korrigieren:
            foreach (var col in columnlist.Columns)
            {
                col.Fieldname = DbUtils.AdjustFieldname(col.Fieldname, EntityFieldlist.Keys.ToList());
            }

            //fehlende Entity Felder als invisible ergänzen:
            //Wenn keine Abfrage/FltrRec dann Standardbereite (width)
            foreach (var field in EntityFieldlist.Keys.ToList())
            {
                var col = columnlist.Columns.Where(x => x.Fieldname == field).FirstOrDefault();
                if (col == null)
                {
                    columnlist.AddColumn($"{field}:{width}={field}");
                }
            }

            //Format bestimmen:
            foreach (var col in columnlist.Columns)
            {
                if (EntityFieldlist.TryGetValue(col.Fieldname, out var fieldInfo))
                {
                    col.FormatString = fieldInfo.Formatstring;
                    col.RzTextAlign = fieldInfo.Options switch
                    {
                        FormatOptions.alRight => TextAlign.Right,
                        FormatOptions.alCenter => TextAlign.Center,
                        _ => TextAlign.Left,
                    };
                    col.SingleStyle = fieldInfo.Options switch
                    {
                        FormatOptions.alRight => "text-align: right",
                        FormatOptions.alCenter => "text-align: center",
                        _ => "text-align: left",
                    };
                }
            }
            return columnlist;
        }


        //KeyFields von Abfrage laden und nach Columnlist.Sortorder übertragenb
        //OrderBy setzen
        public string LoadKeyFields()
        {
            if (FltrRec.KEYFIELDS is null)
                throw new ArgumentNullException(nameof(FltrRec.KEYFIELDS));
            //von Abfrage laden
            //Groß/Kleinschreibung prüfen, anpassen oder Fehler wenn nicht gefunden
            //Bsp  "edt;ETm desc"
            string kf = FltrRec == null ? "" : FltrRec.KEYFIELDS;

            string[] keyfields = kf.Split(";", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            IDictionary<string, string> fields = new Dictionary<string, string>();

            foreach (var keyfield in keyfields)
            {
                var key = keyfield.Split(' ');
                key[0] = DbUtils.AdjustFieldname(key[0], EntityFieldlist.Keys.ToList());  //Groß/Klein korrigieren
                fields.Add(key[0], key.Length >= 2 ? key[1] : "asc");
            }

            //nach Columnlist.Sortorder übertragen: 
            foreach (var col in Columnlist.Columns)
            {
                col.SortOrder = fields.TryGetValue(col.Fieldname, out var keyvalue)
                    ? keyvalue.Equals("asc", CurrentCultureIgnoreCase) 
                        ? SortOrder.Ascending 
                        : SortOrder.Descending
                    : null;
            }

            //OrderBy setzen (Keyfields in Linq Syntax):
            var order = new List<string>();
            foreach (var col in Columnlist.Columns.Where(c => c.SortOrder != null))
            {
                if (col.SortOrder == SortOrder.Descending)
                    order.Add($"{col.Fieldname} {SortOrder.Descending}");
                else
                    order.Add($"{col.Fieldname}");
            }
            OrderBy = string.Join(",", order);

            return kf; //Original KMP Keyfields
        }


        public FltrList LoadFltrlist()
        {
            if (FltrRec.FLTRLIST is null)
                throw new ArgumentNullException(nameof(FltrRec.FLTRLIST));
            //von Abfrage laden: LookUp FLTR[formKurz, Abfrage].FltrList
            //Bsp  "lityp=B;A\r\nlort_nr=57";
            FltrList fltrlist = new(FltrRec.FLTRLIST);  // DB when exists
            //Groß/Klein korrigieren:
            foreach (var fltr in fltrlist.Fltrs)
            {
                fltr.Fieldname = DbUtils.AdjustFieldname(fltr.Fieldname, EntityFieldlist.Keys.ToList());
            }
            return fltrlist;
        }

        #region SQL Where Caluse generieren: Für Dynamic Linq Query

        public string Filter { get; set; }
        public object[] FilterParameters { get; set; }

        public void GenFilter()
        {
            GenFilter(UseFltrs.UseAll);
        }

        public void GenFilter(UseFltrs useFltrs)
        {
            var allFltrlist = new FltrList();
            if (useFltrs == UseFltrs.UseFltrlist || useFltrs == UseFltrs.UseAll)
                allFltrlist.Fltrs.AddRange(Fltrlist.Fltrs);
            if (useFltrs == UseFltrs.UseReferences || useFltrs == UseFltrs.UseAll)
                allFltrlist.Fltrs.AddRange(References.Fltrs);
            //todo: FltrList und References zusammenführen (evtl über die SqlTokens, oder später getrennt zusammenführen:Parameter? )

            //Fltrlist.GenSqlWhere(_filter, _filterparameter);  //schreibt nach _filter und Parameter
            //erstmal ohne Parameter:
            allFltrlist.GenSqlWhere(EntityFieldlist);  //schreibt nach SqlWhere und SqlParams
            Filter = allFltrlist.SqlWhere;
            FilterParameters = [.. allFltrlist.SqlParams.Values];

            //Generate SQL: anhand FltrList und References:
            //works _filter = "(lityp=\"B\" or lityp=\"A\") and (lort_nr=\"57\") and anl_na1 .contains(\"AGH\") and (sta=\"H\")";
            //DynamicFunctions.Like(Brand, \"%a%\")
            //_filter = "(lityp=\"B\" or lityp=\"A\") and (lort_nr=\"57\") and Like(anl_na1, \"%A_H%\" and (sta=\"H\")";
        }

        #endregion

        #region Radzen Grid Parameter, LoadData

        public bool isLoading = false;
        public int Pagesize;
        public bool Paging { get; set; } = true;
        public bool Virtualization { get; set; } = false;
        public int Recordcount { get; set; }  //Istwert von DB
        public IList<TItem> tbl;

        public async Task LoadData(LoadDataArgs args)
        {
            isLoading = true;
            // await Task.Yield();  //why?

            await LoadAbfrageAsync(abfrage);  //von FLTR neu einlesen (Columnlist, Fltrlist,..)

            //Pagesize anpassen falls in GNav/LNav geändert
            if (Paging || Virtualization)
            {
                //verwaltung extra
            }
            else
            {
                Pagesize = Gnav.MaxRecordCount;
                if (args.Skip == 0 && args.Top != Pagesize)
                {
                    Prot.SMessL($"Top: {args.Top} => {Pagesize}");
                    args.Top = Pagesize;
                }
            }
            //merken für später
            if (string.IsNullOrEmpty(args.OrderBy))
                args.OrderBy = OrderBy;  //von Vorgabe
            else
                OrderBy = args.OrderBy;
            Prot.SMessL($"Skip: {args.Skip}, Top: {args.Top}, Pagesize={Pagesize}");

            GenFilter(UseFltrs.UseFltrlist);  //SQL Filter und Parameters generieren. LuDef:nur Fltrlist!
            var query = KmpDbService.QueryFromLoadDataArgs(args);
            query.Filter = Filter;
            query.FilterParameters = FilterParameters;
            Prot.Prot0SL($"Filter:{Filter}");
            Prot.Prot0SL($"Filterparameter:{JsonSerializer.Serialize(query.FilterParameters)}");
            tbl = await KmpDbService.EntityList<TItem>(query);
            //Idee ohne Data Service: tbl = lnav.queryList();

            Recordcount = await KmpDbService.EntityQueryCount<TItem>(query);
            Gnav.GnavChanged();  //Eventconsole
            Prot.SMessL($"Loaded. Count: {Recordcount}");

            isLoading = false;
        }


        #endregion


    }


}
