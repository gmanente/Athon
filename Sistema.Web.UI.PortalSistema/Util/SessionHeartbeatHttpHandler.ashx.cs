using System.Web;
using System.Web.SessionState;
using Sistema.Web.UI.PortalSistema.View.MasterPage;

namespace Sistema.Web.UI.PortalSistema.Util
{
    /// <summary>
    /// Manipulador genérico para manter a sessão sempre viva.
    /// </summary>
    public class SessionHeartbeatHttpHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            //context.Session["SessionPortalAluno"] = Submodulo.GetSession();
        }
    }
}