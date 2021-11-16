using System.Web.Http;
using System.Web.Mvc;

namespace Sistema.Web.UI.Datanorte
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
        }
    }
}