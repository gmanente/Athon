using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Sistema.Web.RepositorioWebAPI.Controller;
using Sistema.Web.RepositorioWebAPI.Tools;

namespace Sistema.Web.RepositorioWebAPI.Attributes
{
    public class AuthorizedAccessAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        // Route Configurations
        public AngularTools.RouteIds RouteIdDefined { get; set; }
        private bool RouteIdConsistent { get; set; }
        private AngularTools.RouteIds RouteIdInformed { get; set; }
        private HttpActionContext httpActionContext { get; set; }
        private AbstractController abstractController { get; set; }
        /// <summary>
        /// Descricao: Requisito(s) Funcional(ais) a serem autenticados para autorização de acesso
        /// Example Usage Sintax:
        /// - #1:
        ///         "First RF with same Controller" | "Second RF with same Controller"
        ///         Ex.: RF001 | RF003.
        ///
        /// - #2:
        ///         "First RF with same Controller" | "Second RF with another Controller" = "Name of Controller"
        ///          Ex.: "RF001 | RF002 = ControllerName"
        ///
        /// - #3:
        ///         "RF to all Controller"
        ///         Ex.: RF001 = ALL.
        /// </summary>
        public string RequisitoFuncional { get; set; }
        private Dictionary<string, string> LstControllerRF { get; set; }
        private string ControllerName { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            httpActionContext = actionContext;
            abstractController = (AbstractController)httpActionContext.ControllerContext.Controller;
            ConfigureProp();

            if (AccessAllowed())
                return;
        }

        private bool AccessAllowed()
        {
            bool accessAllowed = false;
            try
            {

                if (RouteIdConsistent && RouteIdInformed.HasFlag(AngularTools.RouteIds.WC)) return true;

                if (!abstractController.LstPermissions.Any())
                {
                    ResponseStatus(HttpStatusCode.Forbidden);
                    return false;
                }

                if (RouteIdConsistent && RouteIdInformed.HasFlag(AngularTools.RouteIds.WRF)) return true;

                // Check Permission
                //string ControllerName =  //abstractController.ControllerContext.ControllerDescriptor.ControllerName; //abstractController.SessionAngular.ControllerName
                var LstPermissionsByController = LstControllerRF.Where(x => x.Value == ControllerName | x.Value == "ALL");
                var FuncionalidadeAudit = abstractController.LstPermissions.FirstOrDefault(x => LstPermissionsByController.Where(p => p.Key.ToLower() == x.Key).Any());
                accessAllowed = FuncionalidadeAudit.Key != null;

                if (!accessAllowed)
                {
                    ResponseStatus(HttpStatusCode.Unauthorized);
                }
                else
                {
                    AuditOperation.Funcionalidade = FuncionalidadeAudit;
                }

                return accessAllowed;
            }
            catch (Exception)
            {
                //ResponseStatus(HttpStatusCode.BadRequest);
                return false;
            }
        }

        private void ConfigureProp()
        {
            try
            {
                // Check Permission By Route
                if (RouteIdDefined > 0)
                {
                    var HaveRouteIdInformed = abstractController.ActionContext.RequestContext.RouteData.Values.ContainsKey("id");
                    if (HaveRouteIdInformed)
                    {
                        int enumNumb = Convert.ToInt32(abstractController.ActionContext.RequestContext.RouteData.Values["id"]);
                        if (Enum.IsDefined(typeof(AngularTools.RouteIds), enumNumb))
                        {
                            RouteIdInformed = (AngularTools.RouteIds)enumNumb;
                            RouteIdConsistent = RouteIdInformed.HasFlag(RouteIdDefined);
                        }
                    }
                }
                ControllerName = httpActionContext.Request.Headers.GetValues("AuthControl").FirstOrDefault();

                // Contruct object by sintax
                char pipeChar = '|';
                char defineChar = '=';
                var ArrRF = RequisitoFuncional.Split(pipeChar);
                LstControllerRF = ArrRF.ToDictionary(x => x.Split(defineChar).ElementAt(0).Trim(), x => (x.Split(defineChar).ElementAtOrDefault(1) != null ? x.Split(defineChar).ElementAtOrDefault(1).Trim() : abstractController.ControllerContext.ControllerDescriptor.ControllerName));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ResponseStatus(HttpStatusCode st)
        {
            httpActionContext.Response = new HttpResponseMessage(st);
        }
    }
}