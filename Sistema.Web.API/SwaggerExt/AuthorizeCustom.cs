using System.Web.Http;
using System.Web.Http.Controllers;
using Sistema.Web.API;

public class AuthorizeCustom : AuthorizeAttribute
{
    public override void OnAuthorization(HttpActionContext actionContext)
    {
        if (AppSettings.EnableTokenAuthorization == true)
        {
            base.OnAuthorization(actionContext);
        }
    }
}