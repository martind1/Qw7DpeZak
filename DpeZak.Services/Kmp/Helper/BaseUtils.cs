using DpeZak.Services.Kmp.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace DpeZak.Services.Kmp.Helper
{

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
        public static string[] Split2(this string str, string separator, StringSplitOptions options = StringSplitOptions.None)
        {
            var sl = str.Split(separator, options);
            if (sl.Length <= 2)
                return sl;
            string[] sl1 =
            [
                sl[0],
                str[(sl[0].Length + 1)..],  //ohne '='
            ];
            return sl1;
        }

        public static void Debug0()
        {
            //macht nix. Nur für Breakpoint
        }

        #region Appsettings

        /// <summary>
        /// ergibt Zugriff auf appsettings.json
        /// </summary>
        public static IConfiguration Appsettings()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json",
                optional: true, reloadOnChange: false);
            return configurationBuilder.Build();
        }

        public static string ReadSetting(string key, string dflt)
        {
            try
            {
                return Appsettings()[key] ?? dflt;
            }
            catch (ConfigurationErrorsException ex)
            {
                throw new KmpException($"Fehler bei ReadSetting ({key})", ex);
            }
        }

        #endregion
    }

}
