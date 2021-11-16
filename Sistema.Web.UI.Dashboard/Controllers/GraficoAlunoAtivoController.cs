using Sistema.Api.dll.Src.Comum.BE;
using Sistema.Api.dll.Src.Comum.VO;
using Sistema.Api.dll.Src.Coordenacao.BE;
using Sistema.Api.dll.Src.Coordenacao.VO;
using Sistema.Api.dll.Src.SecretariaAcademica.BE;
using Sistema.Api.dll.Src.SecretariaAcademica.VO;
using Sistema.ExtensionApi.dll.Src.Dashboard.BE;
using Sistema.Web.RepositorioWebAPI.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace Sistema.Web.UI.Dashboard.Controllers
{
    public class GraficoAlunoAtivoController : AbstractController
    {
        private PeriodoLetivoBE periodoLetivoBE = null;
        private AcademicoBE academicoBE = null;

        [HttpGet]
        public IHttpActionResult GetPeriodoLetivo(int idPeriodoLetivo)
        {
            try
            {
                periodoLetivoBE = new PeriodoLetivoBE();
                var lst = periodoLetivoBE.Listar(new PeriodoLetivoVO() { Id = idPeriodoLetivo });
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                if (periodoLetivoBE != null)
                    periodoLetivoBE.FecharConexao();
            }
        }

        public TipoUsuarioCoordenacaoVO SelecionarTipoUsuarioCoordenacao(dynamic connBE)
        {
            try
            {
                long idUsusario = Session.IdUsuario;

                TipoUsuarioCoordenacaoVO tipoUsuarioCoordenacaoVO = new TipoUsuarioCoordenacaoVO();

                GerenteAreaBE gerenteAreaBE = new GerenteAreaBE(connBE);
                GerenteAreaVO gerenteAreaVO = gerenteAreaBE.Consultar(new GerenteAreaVO { Usuario = { Id = idUsusario } });

                if (gerenteAreaVO != null)
                {
                    tipoUsuarioCoordenacaoVO.GerenteArea = gerenteAreaVO;
                }
                else
                {
                    AtendenteBE atendenteBE = new AtendenteBE(connBE);
                    AtendenteVO atendenteVO = atendenteBE.Consultar(new AtendenteVO { Usuario = { Id = idUsusario } });

                    if (atendenteVO != null)
                    {
                        tipoUsuarioCoordenacaoVO.Atendente = atendenteVO;
                    }
                    else
                    {
                        CoordenadorBE coordenadorBE = new CoordenadorBE(connBE);
                        CoordenadorVO coordenadorVO = coordenadorBE.Consultar(new CoordenadorVO { Usuario = { Id = idUsusario } });

                        if (coordenadorVO != null)
                        {
                            tipoUsuarioCoordenacaoVO.Coordenador = coordenadorVO;
                        }
                    }
                }

                return tipoUsuarioCoordenacaoVO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IHttpActionResult GetDadosPorPeriodoLetivo(int idCampus, int idPeriodoLetivo)
        {

            try
            {
                academicoBE = new AcademicoBE();

                // Recupera o tipo de usuário da coordenação
                TipoUsuarioCoordenacaoVO tipoUsuarioCoordenacaoVO = SelecionarTipoUsuarioCoordenacao(academicoBE.GetSqlCommand());

                List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();

                bool acessoCompleto = LstPermissions.Where(x => x.Key.ToUpper() == "RF099").Any();

                if (!acessoCompleto && tipoUsuarioCoordenacaoVO.GerenteArea.Id == 0 && tipoUsuarioCoordenacaoVO.Atendente.Id == 0 && tipoUsuarioCoordenacaoVO.Coordenador.Id == 0)
                {
                    throw new Exception("Usuário não possui permissões para acesso");
                }
                else
                {
                    lst = academicoBE.GraficoPeriodoLetivo(
                      idCampus,
                      idPeriodoLetivo,
                      tipoUsuarioCoordenacaoVO.GerenteArea.Id,
                      tipoUsuarioCoordenacaoVO.Coordenador.Id,
                      tipoUsuarioCoordenacaoVO.Atendente.Id,
                      acessoCompleto);
                }

                return Ok(lst);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                if (academicoBE != null)
                    academicoBE.FecharConexao();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (periodoLetivoBE != null)
                periodoLetivoBE.FecharConexao();

            if (academicoBE != null)
                academicoBE.FecharConexao();

            base.Dispose(disposing);
        }
    }
}