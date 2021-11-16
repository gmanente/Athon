using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Sistema.Web.UI.Relatorio.Util
{
    public static class Tool
    {
        /// <summary>
        /// Formata uma data do formato BR para US
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FormatarDataUS(string str)
        {
            if (str == null) return null;

            str = Regex.Replace(str, @"^([0-9]{2})/([0-9]{2})/([0-9]{4})$", "$3-$2-$1");

            return str;
        }

        /// <summary>
        /// Retorna o valor do parâmetro de uma Query String, se existir, ou um valor padrão
        /// </summary>
        /// <param name="this"></param>
        /// <param name="paramName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetParam(this HttpRequest @this, string paramName, string defaultValue = "")
        {
            return @this.QueryString[paramName] ?? defaultValue;
        }

        /// <summary>
        /// Retorna o valor do parâmetro de uma Query String, ou o valor Padrão = ""
        /// </summary>
        /// <param name="this"></param>
        /// <param name="paramName"></param>
        public static string GetStr(this HttpRequest @this, string paramName)
        {
            if (!string.IsNullOrEmpty(@this.QueryString[paramName]))
                return @this.QueryString[paramName];

            return string.Empty;
        }

        /// <summary>
        /// Retorna o valor do parâmetro de uma Query String, ou o valor Padrão = 0
        /// </summary>
        /// <param name="this"></param>
        /// <param name="paramName"></param>
        public static int GetInt(this HttpRequest @this, string paramName)
        {
            if (!string.IsNullOrEmpty(@this.QueryString[paramName]))
                return int.Parse(@this.QueryString[paramName]);

            return 0;
        }

    }
}