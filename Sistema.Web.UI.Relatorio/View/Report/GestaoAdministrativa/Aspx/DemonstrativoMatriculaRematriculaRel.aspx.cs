using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;

namespace Sistema.Web.UI.Relatorio.View.Report.GestaoAdministrativa.Aspx
{
    public partial class DemonstrativoMatriculaRematriculaRel : CommonPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {

            RenovarChecarSessao();
            StiReport report = new StiReport();
            UsuarioFuncionalidadeBE usuarioFuncionalidadeBe = null;

            try
            {
                usuarioFuncionalidadeBe = new UsuarioFuncionalidadeBE();
                Audit.lstUsuarioFuncionalidade = usuarioFuncionalidadeBe.AutenticarFuncionalidades(GetUrlSubModulo(), GetSessao().IdUsuario, GetSessao().IdCampus, GetSessao().AcessoExterno);

                if (Audit.Autenticar("RF001"))
                {
                    report.Load(Server.MapPath("~/View/Report/GestaoAdministrativa/Stimull/DemonstrativoMatriculaRematriculaRel.mrt"));

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("NomeConexao", "NomeConexao", Funcoes.GetArquivoConexaoBanco(), false));
                    report.Compile();

                    int idCampus = Convert.ToInt32(Request.QueryString["idCampus"]);
                    int idPeriodoLetivo = Convert.ToInt32(Request.QueryString["idPeriodoLetivo"]);
                    int calouro = Convert.ToInt32(Request.QueryString["calouro"]);
                    int idCurso = Convert.ToInt32(Request.QueryString["idCurso"]);

                    report.CompiledReport.DataSources["NomeFonteDados"].Parameters["pCampus"].ParameterValue = idCampus;
                    report.CompiledReport.DataSources["NomeFonteDados"].Parameters["pPeriodoLetivo"].ParameterValue = idPeriodoLetivo;
                    report.CompiledReport.DataSources["NomeFonteDados"].Parameters["pCalouro"].ParameterValue = calouro;
                    report.CompiledReport.DataSources["NomeFonteDados"].Parameters["pCurso"].ParameterValue = idCurso;

                    if (calouro == 1)
                    {
                        report["vMatriculaRematricula"] = "Matrícula";
                    }
                    else if (calouro == 0)
                    {
                        report["vMatriculaRematricula"] = "Rematrícula";
                    }
                    else
                    {
                        report["vMatriculaRematricula"] = "Matrícula / Rematrícula";
                    }

                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();

                    stiDemonstrativoMatriculaRematriculaRel.Report = report;
                    //StiReportResponse.ResponseAsPdf(report, false);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (usuarioFuncionalidadeBe != null)
                    usuarioFuncionalidadeBe.FecharConexao();
            }
        }
    }
}