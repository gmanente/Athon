using Sistema.Web.RepositorioWebAPI.Controller;
using Sistema.Web.RepositorioWebAPI.Attributes;
using System;
using System.Web.Http;
using System.Linq;
using Sistema.Api.dll.Src.Seguranca.BE;
using System.Collections.Generic;
using Sistema.Api.dll.Src.Datanorte.VO;
using Sistema.Api.dll.Src.Datanorte.BE;
using Sistema.Api.dll.Src.Seguranca.VO;
using Newtonsoft.Json;

namespace Sistema.Web.UI.Sistema.Controllers
{
    [RoutePrefix("api/Contato")]
    public class ContatoController : AbstractController
    {
        //[AuthorizedAccess(RequisitoFuncional = "RF001 = ALL")]
        [RenewSession]
        [HttpGet]
        public IHttpActionResult ConsultarContato(string nome)
        {
            ContatoBE ContatoBE = null;
            List<ContatoVO> lstContatoVO = null;
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;

            try
            {
                ContatoBE = new ContatoBE();
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE(ContatoBE.GetSqlCommand());
                var idUsuario = Session.IdUsuario;

                var lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(SessionAngular.SubModuloUrl, idUsuario, Session.IdCampus, Session.AcessoExterno);

                lstContatoVO = ContatoBE.Selecionar(
                    new ContatoVO
                    {
                        Nome = nome
                    }).OrderBy(a => a.Nome).ToList();

                return Json(lstContatoVO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                if (ContatoBE != null)
                    ContatoBE.FecharConexao();
            }
        }

        //[AuthorizedAccess(RequisitoFuncional = "RF001")]
        [RenewSession]
        [HttpGet]
        public IHttpActionResult GetAll(string nome, string nomeSocial, string cpf, string rg, string nomeMae, string nomePai, string cidade, string uf)
        {
            ContatoBE contatoBE = null;

            try
            {
                contatoBE = new ContatoBE();

                var contatoVO = new ContatoVO
                {
                    Nome = nome,
                    Nome_Civil = nomeSocial,
                    Nome_Mae = nomeMae,
                    Nome_Pai = nomePai,
                    Cpf = cpf,
                    Rg = rg,
                    Cidade = cidade,
                    Uf = uf
                };

                var lstContatoVO = contatoBE.Selecionar(contatoVO);

                return Json(lstContatoVO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                contatoBE?.FecharConexao();
            }
        }

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

        
    }
}