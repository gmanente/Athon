using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Repositorio.BE;
using Sistema.Web.RepositorioWebAPI.Controller;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Sistema.Web.RepositorioWebAPI.Attributes
{
    public class RenewSessionAttribute : ActionFilterAttribute, IActionFilter, System.Web.SessionState.IRequiresSessionState
    {
        private HttpActionContext httpActionContext { get; set; }
        private HttpActionExecutedContext httpActionExecutedContext { get; set; }
        private AbstractController abstractController { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            httpActionContext = actionContext;
            abstractController = (AbstractController)httpActionContext.ControllerContext.Controller;
            RenewSession();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }

        private bool IsAjaxRequest(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            var _context = HttpContext.Current;

            var isCallbackRequest = false; // Callbacks são solicitações Ajax 
            if (_context != null && _context.CurrentHandler != null && _context.CurrentHandler is System.Web.UI.Page)
                isCallbackRequest = ((System.Web.UI.Page)_context.CurrentHandler).IsCallback;

            return isCallbackRequest || (request["X-Requested-With"] == "XMLHttpRequest") || (request.Headers["X-Requested-With"] == "XMLHttpRequest");
        }

        private void RenewSession()
        {
            AuditoriaBE AuditoriaBe = null;

            try
            {
                AuditoriaBe = new AuditoriaBE();
                var sessao = abstractController.Session;
                long IdModulo = abstractController.IdModulo, IdSubModulo = abstractController.IdSubModulo, IdUsuario = 0;
 
                if (Dominio.AppState != Dominio.ApplicationState.Debug)
                {

                    var _context = HttpContext.Current;

                    HttpRequest request = _context.Request;


                    if (IsAjaxRequest(request))
                    {
                        if (sessao == null)
                        {
                            httpActionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                            return;
                        }
                    }

                    if (sessao == null)
                    {
                        httpActionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        return;
                    }
                    else
                    {
                        IdUsuario = sessao.IdUsuario;

                        var chaveUnicaCookie = request.Cookies["chaveUnica"];
                        var chaveUnica = Guid.Parse(chaveUnicaCookie.Value);

                        if (Dominio.AppState == Dominio.ApplicationState.Producao)
                        {
                            if (!AuditoriaBe.SessaoAtivaChaveUnica(chaveUnica, IdUsuario))
                            {
                                //SessionHandler.RemoveSession("Session");
                                //httpActionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                                //return;   

                                _context.Response.StatusCode = 401;
                                _context.Response.End();

                            }
                        }

                        var s = new SessaoSistema()
                        {
                            IdCampus = sessao.IdCampus,
                            NomeCampus = sessao.NomeCampus,
                            IdUsuario = IdUsuario,
                            IdUsuarioCampus = sessao.IdUsuarioCampus,
                            NomeUsuario = sessao.NomeUsuario,
                            LoginNome = sessao.LoginNome,
                            EmailUsuario = sessao.EmailUsuario,
                            IdAuditoria = sessao.IdAuditoria,
                            IdsCampus = sessao.IdsCampus,
                            IdProfessor = sessao.IdProfessor,
                            IdSubModulo = IdSubModulo,
                            IdModulo = IdModulo,
                            HostName = sessao.HostName
                        };

                        var sessionHandler = new SessionHandler()
                        {
                            Objeto = s
                        };

                        sessionHandler.NewSession("Session");


                        if (IdModulo > 0 && IdSubModulo > 0)
                        {
                            var AuditoriaAtual = AuditoriaBe.Consultar(new AuditoriaVO() { IdUsuario = IdUsuario });
                            var ConsultaBE = new ConsultaBE(AuditoriaBe.GetSqlCommand());
                            var AuditoriaVo = new AuditoriaVO()
                            {
                                Id = AuditoriaAtual.Id,
                                DataLogin = AuditoriaAtual.DataLogin,
                                Login = ConsultaBE.GetLoginSenhaUsuario(AuditoriaAtual.IdUsuario)[0],
                                Senha = ConsultaBE.GetLoginSenhaUsuario(AuditoriaAtual.IdUsuario)[1],
                                SessionId = request.Cookies["ASP.NET_SessionId"].Value, //HttpContext.Current.Request.Cookies["ASP.NET_SessionId"].Value,
                                ServerName = request.ServerVariables["SERVER_NAME"],
                                IdUsuario = AuditoriaAtual.IdUsuario,
                                EnderecoIp = AuditoriaAtual.EnderecoIp,
                                IdCampus = AuditoriaAtual.IdCampus,
                                BrowserVersao = AuditoriaAtual.BrowserVersao,
                                BrowserTipo = AuditoriaAtual.BrowserTipo,
                                BrowserNome = AuditoriaAtual.BrowserNome,
                                IdModulo = IdModulo,
                                IdSubmodulo = IdSubModulo,
                                HostName = AuditoriaAtual.HostName
                            };

                            //Setando config. padroes
                            AuditoriaBe.SetAuditoria(AuditoriaVo);

                            // Verifica se o usuario mantem a sessão no mesmo Modulo (Atualizando somente as Datas e Horas de Ação)
                            if (AuditoriaAtual.IdModulo == AuditoriaVo.IdModulo && AuditoriaAtual.IdSubmodulo == AuditoriaVo.IdSubmodulo)
                                AuditoriaBe.AuditarAcao(AuditoriaVo);
                            else
                                AuditoriaBe.Auditar(AuditoriaVo);
                        }
                    }
                }
                else
                {
                    var consultaBE = new ConsultaBE(AuditoriaBe.GetSqlCommand());
                    var campusBe = new CampusBE(AuditoriaBe.GetSqlCommand());
                    try
                    {
                        var s = new SessaoSistema()
                        {
                            IdCampus = Dominio.IdCampusDebug,
                            NomeCampus = Dominio.NomeCampusDebug,
                            IdUsuario = Dominio.IdUsuarioDebug,
                            IdUsuarioCampus = Dominio.IdUsuarioCampusDebug,
                            NomeUsuario = Dominio.NomeUsuarioDebug,
                            LoginNome = Dominio.SuperAdminDesenvolvimento,
                            EmailUsuario = Dominio.EmailUsuarioDebug,
                            IdsCampus = campusBe.CampusPorUsuario(Dominio.IdUsuarioDebug),
                            IdProfessor = 0, //consultaBE.GetIdProfessor(Dominio.IdUsuarioDebug),
                            IdSubModulo = IdSubModulo,
                            IdModulo = IdModulo
                        };

                        var sessionHandler = new SessionHandler()
                        {
                            Objeto = s
                        };

                        sessionHandler.NewSession("Session");

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (AuditoriaBe != null)
                    AuditoriaBe.FecharConexao();
            }
        }

    }
}