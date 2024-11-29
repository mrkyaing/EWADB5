using System.Net.Sockets;
using System.Net;

namespace CloudHRMS.Utility
{
    public static class NetworkHelper
    {
        //Get Public IP of Machine
        public static string GetMachinePublicIP()
        {
            string ip = "127.0.0.1";
            try
            {
                ip = new WebClient().DownloadString("https://api.ipify.org");
            }
            catch (Exception e)
            {
                ip = GetLocalIPAddress();
            }
            return ip;
        }
        //Get Local IP of Machine
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();//192.1968.1.3 eg
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
