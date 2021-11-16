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
    public class MatriculaRematriculaController : AbstractController
    {
        private PeriodoLetivoBE periodoLetivoBE = null;
        private AcademicoBE academicoBE = null;



        /// <summary>
        /// Autor: Carlos Cortez
        /// Date: 12/12/2017
        /// Description: Método reponsável por Consultar os periodos letivos cadastrados
        /// </summary>
        /// <param name="idPeriodoLetivo"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Autor: Carlos Cortez
        /// Date: 12/12/2017
        /// Description: Método reponsável por consultar os dados de alunos matriculados e rematriculados ativos por área de conhecimento
        /// </summary>
        /// <param name="idPeriodoLetivo"></param>
        /// <param name="idCampus"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDadosAreaConhecimento(int idPeriodoLetivo, int idCampus, bool situacaoAcademicaAtivo = true)
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

                    lst = academicoBE.ChartConsultaQuantitativoAlunoGpa(
                      idPeriodoLetivo,
                      idCampus,
                      tipoUsuarioCoordenacaoVO.GerenteArea.Id,
                      tipoUsuarioCoordenacaoVO.Coordenador.Id,
                      tipoUsuarioCoordenacaoVO.Atendente.Id,
                      acessoCompleto, situacaoAcademicaAtivo: situacaoAcademicaAtivo
                      );

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

        /// <summary>
        /// Autor: Carlos Cortez
        /// Date: 12/12/2017
        /// Description: Método reponsável por consultar os dados de alunos matriculados e rematriculados ativos por curso
        /// </summary>
        /// <param name="idGpa"></param>
        /// <param name="idPeriodoLetivo"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDadosCurso(long idCampus, long idGpa, long idPeriodoLetivo, bool situacaoAcademicaAtivo = true, long IdTipoAcessoIESUnificado = 0)
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
                    lst = academicoBE.ChartConsultaQuantitativoAlunoCurso(idGpa, idPeriodoLetivo,
                      idCampus,
                      tipoUsuarioCoordenacaoVO.GerenteArea.Id,
                      tipoUsuarioCoordenacaoVO.Coordenador.Id,
                      tipoUsuarioCoordenacaoVO.Atendente.Id,
                      acessoCompleto, "0", situacaoAcademicaAtivo, IdTipoAcessoIESUnificado);

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

        /// <summary>
        /// Autor: Carlos Cortez
        /// Date: 12/12/2017
        /// Description: Método reponsável por consultar os dados de alunos matriculados e rematriculados ativos por Tipo de Acesso IES
        /// </summary>
        /// <param name="idGpa"></param>
        /// <param name="idPeriodoLetivo"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDadosTipoAcessoIES(long idCampus, int idGpa, int idPeriodoLetivo)
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
                    lst = academicoBE.ChartConsultaQuantitativoAlunoTipoIngressoIES(idGpa, idPeriodoLetivo,
                      idCampus,
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

        /// <summary>
        /// Autor: Carlos Cortez
        /// Date: 12/12/2017
        /// Description: Método responsável por listar todos os tipos de acesso
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllTipoAcessoIES()
        {

            try
            {
                academicoBE = new AcademicoBE();

                List<TipoAcessoIESVO> lst = new List<TipoAcessoIESVO>();

                TipoAcessoIESBE tipoAcessoIESBE = new TipoAcessoIESBE(academicoBE.GetSqlCommand());

                lst = tipoAcessoIESBE.Listar();
                return Ok(lst.OrderBy(x => x.Nome));
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


        /// <summary>
        /// Autor: Carlos Cortez
        /// Date: 12/12/2017
        /// Description: Método reponsável por consultar os dados de alunos matriculados e rematriculados ativos por Turma
        /// </summary>
        /// <param name="idGpa"></param>
        /// <param name="idPeriodoLetivo"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDadosCursoTurma(int idCurso, int idPeriodoLetivo, bool situacaoAcademicaAtivo = true, long IdTipoAcessoIESUnificado = 0)
        {

            try
            {
                academicoBE = new AcademicoBE();
                var lst = academicoBE.ChartConsultaQuantitativoAlunoCursoTurma(idCurso, idPeriodoLetivo, situacaoAcademicaAtivo, IdTipoAcessoIESUnificado);


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

        /// <summary>
        /// Autor: Carlos Cortez
        /// Date: 12/01/2018
        /// Description: Método reponsável por consultar os aluno matriculas na turma
        /// </summary>
        /// <param name="idGpa"></param>
        /// <param name="idPeriodoLetivo"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDadosAlunoCursoTurma(
            int idCampus, int idPeriodoletivo, int idGradeLetivaTurma, bool situacaoAcademicaAtivo = true, long IdTipoAcessoIESUnificado = 0)
        {
            try
            {
                academicoBE = new AcademicoBE();
                var lst = academicoBE.ListarAlunoSemestreTurma(idCampus, idPeriodoletivo, idGradeLetivaTurma, situacaoAcademicaAtivo, IdTipoAcessoIESUnificado);
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



        /// <summary>
        /// Autor: Carlos Cortez
        /// Date: 15/01/2018
        /// Description: Método responsável por consultar o total de matricula e rematricula por periodo
        /// </summary>
        /// <param name="idCampus"></param>
        /// <param name="idPeriodoLetivo"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDadosResumoPorPeriodo(int idCampus, int idPeriodoLetivo)
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
                    lst = academicoBE.ChartConsultaResumoPorPeriodo(
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



        /// <summary>
        /// Autor: Carlos Cortez
        /// Date: 15/01/2018
        /// Description: Método responsável por consultar o comparativo de calouros e veteranos
        /// </summary>
        /// <param name="idCampus"></param>
        /// <param name="idPeriodoLetivo"></param>
        /// <returns></returns>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDadosCalouroVeterano(int idCampus, int idPeriodoLetivo)
        {

            try
            {
                academicoBE = new AcademicoBE();
                var lst = academicoBE.ChartConsultaResumoCalouroVeterano(idCampus, idPeriodoLetivo);



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





        /// <summary>
        /// Autor: Carlos Cortez
        /// Date: 12/12/2017
        /// Description: Método reponsável por consultar os dados de alunos matriculados e rematriculados ativos por Tipo de Acesso IES
        /// </summary>
        /// <param name="idGpa"></param>
        /// <param name="idPeriodoLetivo"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDadosSituacaoAcademica(long idCampus, int idGpa, int idPeriodoLetivo, bool situacaoAcademicaAtivo = true)
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
                    lst = academicoBE.ChartConsultaQuantitativoAlunoPorSituacaoAcademica(idGpa, idPeriodoLetivo,
                      idCampus,
                      tipoUsuarioCoordenacaoVO.GerenteArea.Id,
                      tipoUsuarioCoordenacaoVO.Coordenador.Id,
                      tipoUsuarioCoordenacaoVO.Atendente.Id,
                      acessoCompleto, "0", situacaoAcademicaAtivo);

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


        /// <summary>
        /// Autor: Carlos Cortez
        /// Date: 12/12/2017
        /// Description: Metodo responsável por descarregar as conexões ativas
        /// </summary>
        /// <param name="disposing"></param>
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