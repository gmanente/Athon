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
    public class CalouroVeteranoController : AbstractController
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
        public IHttpActionResult GetPeriodoLetivo(int idPeriodoLetivo, string descricao, int idPeriodoLetivoAtual)
        {

            try
            {
                periodoLetivoBE = new PeriodoLetivoBE();
                var lst = periodoLetivoBE.Listar(new PeriodoLetivoVO() { Id = idPeriodoLetivo });

                if (!string.IsNullOrEmpty(descricao))
                    lst = lst
                        .Where(x => x.Descricao.Contains(descricao))
                        .Where(x=> idPeriodoLetivoAtual == 0 || x.Id < idPeriodoLetivoAtual)
                        .ToList();

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