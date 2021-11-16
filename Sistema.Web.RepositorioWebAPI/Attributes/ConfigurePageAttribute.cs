using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Repositorio.BE;
using Sistema.Api.dll.Src.Seguranca.BE;
using System;
using System.Linq;
using System.Web;
using static Sistema.Web.RepositorioWebAPI.Tools.AngularTools;

namespace Sistema.Web.RepositorioWebAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurePageAttribute : Attribute
    {
        public string Controller { get; set; }
        public string Module { get; set; }
        private string SubModuloUrl { get; set; }
        private SessaoSistema Session { get; set; }
        public ConfigurePageAttribute()
        {
            ConfigureProp();
        }

        public ConfigurePageAttribute(string module, string controller = null, bool portal = false)
        {
            Module = module;
            Controller = controller;

            if (portal)
                ConfigurePropPortal();
            else
                ConfigureProp();
        }

        private string GetSubModuloUrl()
        {
            string url = HttpContext.Current.Request.Url.ToString();
            var UrlSemQueryString = new Uri(url).GetLeftPart(UriPartial.Path);
            var arrUrl = UrlSemQueryString.Split('/');
            string newUrl = "";

            for (var i = 0; i < arrUrl.Length; i++)
            {
                var urlUnit = arrUrl[i];

                if (i == 0)
                    newUrl += arrUrl[i] + "//";
                else if (i > 1)
                    newUrl += arrUrl[i] + "/";
            }
            var subModuloUrl = newUrl.Substring(0, newUrl.IndexOf("aspx") + 4);

            // Altera a urlSubModulo funcionais para atender multiplos protocolos
            // Data alteração: 20/07/2015
            // Alterado por: Evander
            subModuloUrl = subModuloUrl.Replace("https:", "").Replace("http:", "");

            return subModuloUrl;
        }

        private SessaoSistema GetSession()
        {
            try
            {
                if (Dominio.AppState == Dominio.ApplicationState.Debug)
                    SetSessionDebug();

                var cookie = HttpContext.Current.Request.Cookies[Funcoes.RemoveCaracteresEspeciais(Criptografia.CifrarCesar(Dominio.SessionName, Dominio.SessionName.Length), false, false)];
                if (cookie != null)
                {
                    return Json.DeSerialize<SessaoSistema>(Criptografia.Base64Decode(HttpUtility.UrlDecode(cookie.Value)));
                }
                else throw new Exception("SE");

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ConfigureProp()
        {
            try
            {
                SubModuloUrl = GetSubModuloUrl();
                Session = GetSession();

                SetAngularSession();
                SetRouteIdsSession();
                SetAllPermissions();
            }
            catch (Exception ex)
            {
                if (ex.Message == "SE") HttpContext.Current.Response.Redirect("Erro.aspx?s=sessao-expirada");
                else HttpContext.Current.Response.Redirect("Erro.aspx?s=500&msg=" + ex.Message);
            }
        }


        private void ConfigurePropPortal()
        {
            try
            {
                SubModuloUrl = "";

                SetAngularSession();

                SetRouteIdsSession();

                SetAllPermissions();
            }
            catch (Exception)
            {
                //if (ex.Message == "SE") HttpContext.Current.Response.Redirect("Erro.aspx?s=sessao-expirada");
                //else HttpContext.Current.Response.Redirect("Erro.aspx?s=500&msg=" + ex.Message);
            }
        }


        private void SetAngularSession()
        {
            try
            {
                if (!string.IsNullOrEmpty(Module))
                {
                    var angularSession = new AngularSession();

                    if (string.IsNullOrEmpty(Controller))
                        Controller = HttpContext.Current.Request.Path.Substring(HttpContext.Current.Request.Path.LastIndexOf("/") + 1).Replace(".aspx", "");

                    angularSession.ControllerName = Controller;
                    angularSession.ModuleName = Module;
                    angularSession.SubModuloUrl = SubModuloUrl;
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie(Criptografia.MD5("AngularSession"), Criptografia.Base64Encode((Json.Serialize(angularSession)))));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void SetRouteIdsSession()
        {
            try
            {
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(Criptografia.MD5("RouteIdsSession"), Criptografia.Base64Encode((EnumToJson(typeof(RouteIds))))));
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void SetAllPermissions()
        {
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;
            try
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                var LstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(SubModuloUrl, Session.IdUsuario, Session.IdCampus, Session.AcessoExterno);
                LstUsuarioFuncionalidade.ForEach(x => x.Funcionalidade.RequisitoFuncional = x.Funcionalidade.RequisitoFuncional.ToLower());
                var LstPermissions = LstUsuarioFuncionalidade.ToDictionary(x => x.Funcionalidade.RequisitoFuncional, p => p.Funcionalidade.Id);

                HttpContext.Current.Response.Cookies.Add(new HttpCookie(Criptografia.MD5("AllPermissions_" + Controller), Criptografia.Base64Encode((Json.Serialize(LstPermissions)))));

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (usuarioFuncionalidadeBe != null)
                    usuarioFuncionalidadeBe.FecharConexao();
            }
        }


        private void SetSessionDebug()
        {
            var consultaBE = new ConsultaBE();
            var campusBe = new CampusBE(consultaBE.GetSqlCommand());
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
                    IdSubModulo = 0,
                    IdModulo = 0
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
            finally
            {
                if (campusBe != null)
                    campusBe.FecharConexao();
            }
        }
    }
}

