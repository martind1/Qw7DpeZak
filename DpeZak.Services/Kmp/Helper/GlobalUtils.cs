using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DpeZak.Services.Kmp.Helper
{
    public class GlobalUtils
    {
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
                Log.Warning(ex, $"GetMachineNameFromIPAddress({ipAddress})");
                // Machine not found...
                machineName = ipAddress;
            }
            return machineName;
        }

    }
}
