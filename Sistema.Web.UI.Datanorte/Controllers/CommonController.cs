using System;
using System.Collections.Generic;
using System.Web.Http;
using Sistema.Web.RepositorioWebAPI.Attributes;
using Sistema.Web.RepositorioWebAPI.Controller;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using System.Linq;

namespace Sistema.Web.UI.Datanorte.Controllers
{
    [RoutePrefix("api/Common")]
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
        public IHttpActionResult GetEstado()
        {
            EstadoBE EstadoBe = null;
            List<EstadoVO> lstEstadoVO = null;

            try
            {
                EstadoBe = new EstadoBE();
                lstEstadoVO = EstadoBe.Selecionar(new EstadoVO());
                return Json(lstEstadoVO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                if (EstadoBe != null)
                    EstadoBe.FecharConexao();
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

        //// GET:
        //[RenewSession]
        //[Route("GetPessoaByCpfCnpj/{cpfCnpj}")]
        //[HttpGet]
        //public IHttpActionResult GetPessoaByCpfCnpj(string cpfCnpj)
        //{
        //    PessoaBE PessoaBe = null;

        //    try
        //    {
        //        PessoaBe = new PessoaBE();
        //        var PessoaVo = PessoaBe.Consultar(new PessoaVO() { CpfCnpj = cpfCnpj });
        //        return Json(PessoaVo);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    finally
        //    {
        //        if (PessoaBe != null)
        //            PessoaBe.FecharConexao();
        //    }
        //}

        // GET: 
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

        
    }
}