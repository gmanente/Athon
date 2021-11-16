using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Web.RepositorioWebAPI.Attributes.GenerateForms;

namespace Sistema.Web.RepositorioWebAPI.Tools
{
    public class WebTools
    {
        public const int CookieExpirationMinutes = 60;
        public static T GetCookie<T>(string key) where T : class
        {
            try
            {
                var cookie = HttpContext.Current.Request.Cookies[Criptografia.MD5(key)];
                if (cookie != null)
                {
                    return JsonConvert.DeserializeObject<T>(Criptografia.Base64Decode(cookie.Value));
                }
                else return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void SetCookie<T>(string key, T value) where T : class
        {
            try
            {
                DeleteCookie<T>(key);
                var cookie = new HttpCookie(Criptografia.MD5(key), Criptografia.Base64Encode((JsonConvert.SerializeObject(value))));

                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void DeleteCookie<T>(string key) where T : class
        {
            try
            {
                var cookie = HttpContext.Current.Request.Cookies[Criptografia.MD5(key)];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-2d);
                    HttpContext.Current.Response.SetCookie(cookie);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static bool HasAttribute(PropertyInfo prop, Type attributeType)
        {
            var bo = prop.GetCustomAttribute(attributeType);
            return bo != null;
        }

        public static string GetAttributeValue(PropertyInfo prop, Type attributeType, string propName = "Rules")
        {
            var bo = prop.GetCustomAttribute(attributeType);
            return bo.GetType().GetProperty(propName).GetValue(bo).ToString();
        }
    }
}