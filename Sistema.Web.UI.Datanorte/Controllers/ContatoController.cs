using System;
using System.Web.Http;
using Sistema.Api.dll.Src.Datanorte.BE;
using Sistema.Api.dll.Src.Datanorte.VO;
using Sistema.Web.RepositorioWebAPI.Attributes;
using Sistema.Web.RepositorioWebAPI.Controller;
using System.Linq;
using util = Sistema.Api.dll.Repositorio.Util;
using Newtonsoft.Json.Linq;
using Sistema.Api.dll.Src.Seguranca.BE;
using System.Collections.Generic;

namespace Sistema.Web.UI.Datanorte.Controllers
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
        public IHttpActionResult GetAll(string nome, string cpf, string cep, string bairro, string logradouroNumero, string Logradouro, string telefone, string dddFinal, string dddInicial, string cidade, string uf)
        {                              
            ContatoBE contatoBE = null;

            try
            {
                contatoBE = new ContatoBE();

                var contatoVO = new ContatoVO
                {
                   Nome =  nome, 
                    Cpf = cpf,
                    Cep = cep,
                    Bairro = bairro,
                    Logr_Numero = logradouroNumero,
                    Logr_Nome = Logradouro,
                    TelefoneNumero = telefone,
                    DDDFinal= dddFinal,
                    DDDInicial = dddInicial,
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

        [AuthorizedAccess(RequisitoFuncional = "RF004")]
        [RenewSession]
        [HttpGet]
        public IHttpActionResult GetHistorico(int id)
        {
            ContatoHistoricoBE contatoHistoricoBE = null;
            List<ContatoHistoricoVO> lstContatoHistoricoVO = null;

            try
            {
                contatoHistoricoBE = new ContatoHistoricoBE();

                lstContatoHistoricoVO = contatoHistoricoBE.Listar(new ContatoHistoricoVO { Contatos_id = id  });

                return Json(lstContatoHistoricoVO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                if (contatoHistoricoBE != null)
                    contatoHistoricoBE.FecharConexao();
            }
        }

        [AuthorizedAccess(RequisitoFuncional = "RF003")]
        [RenewSession]
        [HttpPost]
        public IHttpActionResult Insert([FromBody] ContatoHistoricoVO ContatoHistorico)
        {
            ContatoHistoricoBE ContatoHistoricoBE  = null;
            try
            {
                long id = 0;

                ContatoHistoricoBE = new ContatoHistoricoBE();
                var idUsuario = Session.IdUsuario;

                ContatoHistorico.Usuario.Id = idUsuario;

                if (ContatoHistorico.Dataoperacao == null)
                    ContatoHistorico.Dataoperacao = DateTime.Now;

                

                id = ContatoHistoricoBE.Inserir(ContatoHistorico);

                if (id == -1)
                {
                    return Conflict();
                }
                else
                {                    
                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                if (ContatoHistoricoBE != null)
                    ContatoHistoricoBE.FecharConexao();
            }
        }

        [RenewSession]
        [HttpGet]
        public IHttpActionResult GetCidade(string uf)
        {
            CidadeBE CidadeBe = null;
            List<CidadeVO> lstCidadeVO = null;

            try
            {
                CidadeBe = new CidadeBE();
                lstCidadeVO = CidadeBe.Selecionar(new CidadeVO()
                { 
                    UF = uf
                });
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
        public IHttpActionResult GetBairro(string uf, string cidade)
        {
            BairroBE BairroBe = null;
            List<BairroVO> lstBairroVO = null;

            try
            {
                BairroBe = new BairroBE();
                lstBairroVO = BairroBe.Selecionar(new BairroVO()
                {
                    UF = uf
                    ,Cidade = cidade
                });
                return Json(lstBairroVO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                if (BairroBe != null)
                    BairroBe.FecharConexao();
            }
        }
    }
}