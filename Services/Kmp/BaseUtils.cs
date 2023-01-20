namespace QwTest7.Services.Kmp
{
    /// <summary>
    /// Basis Exception für unsere Anwendung
    /// </summary>
    [System.Serializable]
    public class KmpException : Exception
    {
        public KmpException() { }
        public KmpException(string message) : base(message) { }
        public KmpException(string message, Exception inner) : base(message, inner) { }
        protected KmpException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Statische Klasse mit Anwendungs Konstanten
    /// </summary>
    public static class AppParameter
    {
        //für ProtService
        public static int MaxStatusListEntries { get; set; } = 1000;
    }

    /// <summary>
    /// Statische Klasse mit Basis Hilfsfunktionen
    /// </summary>
    public static class BaseUtils
    {

        /// <summary>
        /// Splittet String in 2 Teile. 1.Teil: Erstes Token  2.Teil: Rest
        /// Bsp: 'Fld1=a=b' -> ('Fld1', 'a=b')
        /// </summary>
        /// <returns>Stringliste mit 2 Einträgen</returns>
        public static String[] Split2(this string str, String separator, StringSplitOptions options = StringSplitOptions.None)
        {
            var sl = str.Split(separator, options);
            if (sl.Length <= 2)
                return sl;
            string[] sl1 = new string[2];
            sl1[0] = sl[0];
            sl1[1] = str[(sl[0].Length + 1)..];  //ohne '='
            return sl1;
        }

        public static void Debug0()
        {
            //macht nix. Nur für Breakpoint
        }
    }

}
