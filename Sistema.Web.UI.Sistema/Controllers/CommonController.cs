using System;
using System.Collections.Generic;
using System.Web.Http;
using Sistema.Web.RepositorioWebAPI.Attributes;
using Sistema.Web.RepositorioWebAPI.Controller;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Newtonsoft.Json;
using System.Linq;
using static Sistema.Web.RepositorioWebAPI.Tools.AngularTools;
using Sistema.Web.RepositorioWebAPI.Attributes;
using Sistema.Web.RepositorioWebAPI.Tools;
using Sistema.Api.dll.Src.Repositorio.BE;

namespace Sistema.Web.UI.Sistema.Controllers
{
    public class CommonController : AbstractController
    {

        // GET:
        [RenewSession]
        [Route("GetCidades/{nomeCidade}")]
        [HttpGet]
        public IHttpActionResult GetCidades(string nomeCidade)
        {
            CidadeBE CidadeBe = null;
            List<CidadeVO> lstCidadeVO = null;

            try
            {
                CidadeBe = new CidadeBE();
                lstCidadeVO = CidadeBe.Selecionar(new CidadeVO() { Nome = nomeCidade });
                return Json(lstCidadeVO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                if (CidadeBe != null)
                    CidadeBe.FecharConexao();
            }
        }

        [RenewSession]
        [HttpGet]
        public IHttpActionResult GetCidadeById(int id)
        {
            CidadeBE CidadeBe = null;

            try
            {
                CidadeBe = new CidadeBE();
                var cidadeVO = CidadeBe.Consultar(new CidadeVO() { Id = id });
                return Json(cidadeVO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                if (CidadeBe != null)
                    CidadeBe.FecharConexao();
            }
        }

        [RenewSession]
        [HttpGet]
        public IHttpActionResult GetDepartamentoUsuario()
        {
            UsuarioDepartamentoBE UsuarioDepartamentoBE = null;
            List<UsuarioDepartamentoVO> lstUsuarioDepartamentoVO = null;

            var IdUsuario = Session.IdUsuario;

            try
            {
                UsuarioDepartamentoBE = new UsuarioDepartamentoBE();

                lstUsuarioDepartamentoVO = UsuarioDepartamentoBE.SelecionarUsuarioDepartamento(new UsuarioDepartamentoVO { Usuario = { Id = Session.IdUsuario } });

                return Json(lstUsuarioDepartamentoVO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                if (UsuarioDepartamentoBE != null)
                    UsuarioDepartamentoBE.FecharConexao();
            }
        }

        [RenewSession]
        [HttpGet]
        public IHttpActionResult GetListCampusUsuario()
        {
            CampusBE campusBe = null;
            List<CampusVO> lstCampusVo = null;

            var IdUsuario = Session.IdUsuario;

            try
            {
                campusBe = new CampusBE();

                lstCampusVo = campusBe.SelecionarPorUsuario(IdUsuario).GroupBy(x => x.Id).Select(x => x.First()).ToList();

                return Ok(lstCampusVo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            finally
            {
                if (campusBe != null)
                    campusBe.FecharConexao();
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

    }
}