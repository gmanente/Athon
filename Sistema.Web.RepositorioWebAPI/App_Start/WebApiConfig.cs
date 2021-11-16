using System.Web.Http;

namespace Sistema.Web.RepositorioWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            //config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings();
            //config.MapHttpAttributeRoutes();
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
