namespace QwTest7.Portal.Services.Kmp.Enums
{
    [Flags]
    public enum FormatOptions
    {
        alNone = 0,
        alRight = 1,          // r,  RzTextAlign := taRightJustify
        alLeft = 2,           // l,  RzTextAlign := taLeftJustify
        alCenter = 4,         // c,  RzTextAlign := taCenter
        alIgnoreName = 8,     //IGN, Fehlenden Feldnamen ignorieren
        alAutoGenerate = 16,  //A,   vom Server verwalteter Vorgabewert
        alRequired = 32,      //N,   Mussfeld
        alReadOnly = 64,      //R,   Nur-Lesen Feld
        alInternalCalc = 128, //C,   Feld wird in der Datenbank berechnet (nicht in DB speichern)
        alInt = 256,          //INT, Werte in Sql als Integer formatieren (statt string)->sql: F=12->(int)F=12
        alTrimLeft0 = 512,    //TL0, 0en links weg
        alAsw = 1024,         //ASW,<a>  a=Auswahlname  
    }




}