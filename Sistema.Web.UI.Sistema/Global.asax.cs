using System;

namespace Sistema.Web.UI.Sistema
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            string sessionId = Session.SessionID;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
            ///
            //HttpContext.Current.Request.Cookies.Remove("Wmx6enB2dQ");
            //HttpContext.Current.Request.Cookies.Remove("Wmx6enB2dQgJ");
        }
    }
}