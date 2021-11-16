using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Sistema.Api.dll.Repositorio.Util;
using Util = Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src;
using Sistema.Api.dll.Src.Repositorio.BE;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Sistema.Web.RepositorioWebAPI.Attributes;
using Sistema.Web.RepositorioWebAPI.Tools;
using static Sistema.Web.RepositorioWebAPI.Tools.AngularTools;

namespace Sistema.Web.RepositorioWebAPI.Controller
{
    public abstract class AbstractController : ApiController
    {
        public SessaoSistema Session { get; set; }
        public AngularSession SessionAngular { get; set; }
        public Dictionary<string, long> LstPermissions { get; set; }
        public long IdSubModulo { get; set; }
        public long IdModulo { get; set; }

        public AbstractController()
        {
            ConfigureProp();
        }

        private void ConfigureProp()
        {
            try
            {
                var cookieSession = HttpContext.Current.Request.Cookies[Funcoes.RemoveCaracteresEspeciais(Criptografia.CifrarCesar(Dominio.SessionName, Dominio.SessionName.Length), false, false)];

                Session = Util.Json.DeSerialize<SessaoSistema>(Criptografia.Base64Decode(HttpUtility.UrlDecode(cookieSession.Value)));

                var cookieAngularSession = HttpContext.Current.Request.Cookies[HttpUtility.UrlEncode(Criptografia.MD5("AngularSession"))];

                if (cookieAngularSession == null)
                    throw new UnauthorizedAccessException();

                SessionAngular = Util.Json.DeSerialize<AngularSession>(Criptografia.Base64Decode(cookieAngularSession.Value));

                string ctrl = HttpContext.Current.Request.Headers.Get("AuthControl");

                var cookieAllPermissions = HttpContext.Current.Request.Cookies[HttpUtility.UrlEncode(Criptografia.MD5("AllPermissions_" + ctrl))];

                LstPermissions = Util.Json.DeSerialize<Dictionary<string, long>>(Criptografia.Base64Decode(cookieAllPermissions?.Value));

                IdSubModulo = GetIdSubModulo();
                IdModulo = GetIdModulo();
            }
            catch (Exception)
            {
                ResponseActionContext(HttpStatusCode.Unauthorized);
            }
        }


        private long GetIdSubModulo()
        {
            ConsultaBE consultaBE = null;

            try
            {
                consultaBE = new ConsultaBE();

                return consultaBE.GetIdSubModulo(SessionAngular.SubModuloUrl);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (consultaBE != null)
                    consultaBE.FecharConexao();
            }
        }


        private long GetIdModulo()
        {
            ConsultaBE consultaBE = null;

            try
            {
                consultaBE = new ConsultaBE();

                return consultaBE.GetIdModulo(GetIdSubModulo());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (consultaBE != null)
                    consultaBE.FecharConexao();
            }
        }


        public IHttpActionResult ResponseStatus(HttpStatusCode st)
        {
            return ResponseMessage(new HttpResponseMessage(st));
        }


        public void ResponseActionContext(HttpStatusCode st)
        {
            ActionContext.Response = new HttpResponseMessage(st);
        }


        // GET: api/Seguranca/GetCurrentUser
        [RenewSession]
        [HttpGet]
        public IHttpActionResult GetCurrentUser()
        {
            UsuarioBE usuarioBE = null;
            try
            {
                usuarioBE = new UsuarioBE();

                var Usuario = usuarioBE.Consultar(new UsuarioVO() { Id = Session.IdUsuario });

                return Json(Usuario, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                if (usuarioBE != null)
                    usuarioBE.FecharConexao();
            }
        }


        // GET: api/Seguranca/GetPermissions
        [RenewSession]
        [HttpGet]
        public IHttpActionResult GetPermissions()
        {
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBE = null;
            try
            {
                usuarioFuncionalidadeBE = new UsuarioFuncionalidadeBE();

                var lstUsuarioFuncionalidade = usuarioFuncionalidadeBE.AutenticarFuncionalidades(SessionAngular.SubModuloUrl, Session.IdUsuario, Session.IdCampus, Session.AcessoExterno);

                return Json(lstUsuarioFuncionalidade, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                if (usuarioFuncionalidadeBE != null)
                    usuarioFuncionalidadeBE.FecharConexao();
            }
        }


        // GET: api/Seguranca/GetSubmodulosAutenticados
        [RenewSession]
        [HttpGet]
        public IHttpActionResult GetSubmodulosAutenticados()
        {
            UsuarioModuloBE usuarioModuloBE = null;
            try
            {
                usuarioModuloBE = new UsuarioModuloBE();

                List<UsuarioModuloVO> lstUsuarioModulo = null;
                long idModulo = GetIdModulo();
                if (idModulo > 0)
                    lstUsuarioModulo = usuarioModuloBE.AutenticarModulos(Session.IdUsuario, Session.IdCampus, Session.AcessoExterno, null, idModulo);

                return Ok(lstUsuarioModulo);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            finally
            {
                if (usuarioModuloBE != null)
                    usuarioModuloBE.FecharConexao();
            }
        }


        // GET: api/Seguranca/GetCampusUsuario
        [RenewSession]
        [HttpGet]
        public IHttpActionResult GetCampusUsuario()
        {
            UsuarioCampusBE usuarioCampusBE = null;
            try
            {
                usuarioCampusBE = new UsuarioCampusBE();

                var lstUsuarioCampus = usuarioCampusBE.Listar(new UsuarioCampusVO() { Usuario = { Id = Session.IdUsuario } }, true);
                //lstUsuarioCampus = lstUsuarioCampus.OrderBy(o => o.Campus.Nome).ToList();

                return Json(lstUsuarioCampus, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (usuarioCampusBE != null)
                    usuarioCampusBE.FecharConexao();
            }
        }


        public void Audit(string tabela, long codigo, string colunareferencia = null, bool openTran = true)
        {
            AuditOperation.Session = Session;
            AuditOperation.SessionAngular = SessionAngular;
            AuditOperation.Audit(tabela, codigo, colunareferencia, openTran);
        }


        public void Audit(string rf, string tabela, long codigo, string colunareferencia = null, bool openTran = false)
        {
            AuditOperation.Audit(rf, tabela, codigo, colunareferencia, openTran);
        }


        public static string EncapsuledError(Exception ex)
        {
            if (ex.Message.Contains("REFERENCE constraint"))
            {
                return "Não é possível excluir o registro, pois o mesmo possui referencia com outros registros";
            }

            return ex.Message;
        }

    }
}