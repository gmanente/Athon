using System;
using System.Net;

namespace Sistema.Api.dll.Repositorio.Util
{
    /// <summary>
    ///  Author......: Michael S. Lopes , Leandro Curioso
    ///  Date........: 08/01/2014
    ///  Description.: Classe resposavel por códigos referentes a IP
    /// </summary>
    public class Ip
    {

        /// <summary>
        /// Author......: Leandro Curioso , Michael S. Lopes
        /// Date........: 08/01/2014
        /// </summary>
        /// <param name="hostName">O método recebe uma string com o nome da máquina</param>
        /// <returns>Retorna uma string com o IP</returns>
        public static string GetIp(string hostName)
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
            IPAddress[] addr = ipEntry.AddressList;
            String strIP = String.Empty;
            for (int i = 0; i < addr.Length; i++)
            {
                strIP = strIP + addr[i].ToString() + " - ";
            }
            return strIP.Substring(0, strIP.Length-2);
        }

        /// <summary>
        /// Author......: Leandro Curioso , Michael S. Lopes
        /// Date........: 08/01/2014
        /// </summary>
        /// <returns>Retorna uma string com o IP</returns>
        public static string GetIp()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
            IPAddress[] addr = ipEntry.AddressList;
            String strIP = String.Empty;
            for (int i = 0; i < addr.Length; i++)
            {
                strIP = strIP + addr[i].ToString();
            }
            return strIP.Substring(0, strIP.Length - 2);
        }

        public static string GetClientIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

    }
}
