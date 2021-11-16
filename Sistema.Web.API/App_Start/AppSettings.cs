using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Sistema.Web.API
{
    public class AppSettings
    {
        public static NameValueCollection appSettings { get; set; }

        // WEB API CONFIG
        public static bool EnableMultipleApiVersions;
        public static bool EnableTokenAuthorization;
        public static bool EnableOnlyJsonResponseType;

        /// <summary>
        /// Use o seguinte código para gerar uma Chave Secreta simétrica
        ///     var hmac = new HMACSHA256();
        ///     var key = Convert.ToBase64String(hmac.Key);
        /// </summary>
        public static string SecretKey = "";

        public static void Register()
        {
            // Web.config
            appSettings = ConfigurationManager.AppSettings;

            if (appSettings.Count > 0)
            {
                EnableMultipleApiVersions = Convert.ToBoolean(appSettings["EnableMultipleApiVersions"]);
                EnableTokenAuthorization = Convert.ToBoolean(appSettings["EnableTokenAuthorization"]);
                EnableOnlyJsonResponseType = Convert.ToBoolean(appSettings["EnableOnlyJsonResponseType"]);
                SecretKey = Convert.ToString(appSettings["SecretKey"]);
            }
        }
    }
}