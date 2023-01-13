namespace QwTest7.Services.Kmp
{
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

    // Statische Klasse mit Basis Hilfsfunktionen
    public static class BaseUtils
    {

        //Splittet String in 2 Teile. 1.Teil: Erstes Token  2.Teil: Rest
        //Bsp: 'Fld1=a=b' -> ('Fld1', 'a=b')
        public static String[] Split2(this string str, String separator, StringSplitOptions options = StringSplitOptions.None)
        {
            var sl = str.Split(separator, options);
            if (sl.Length <= 2)
                return sl;
            string[] sl1 = new string[2];
            sl1[0] = sl[0];
            sl1[1] = str.Substring(sl[0].Length + 1);  //ohne '='
            return sl1;
        }

        public static void Debug0()
        {
            //macht nix
        }
    }

}
