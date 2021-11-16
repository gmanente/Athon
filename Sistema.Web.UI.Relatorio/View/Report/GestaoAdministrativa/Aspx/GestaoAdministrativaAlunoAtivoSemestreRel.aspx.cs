using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;

namespace Sistema.Web.UI.Relatorio.View.Report.GestaoAdministrativa.Aspx
{
    public partial class GestaoAdministrativaAlunoAtivoSemestreRel : CommonPage
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

                if (Audit.Autenticar("RF007"))
                {
                    report.Load(Server.MapPath("~/View/Report/GestaoAdministrativa/Stimull/GestaoAdministrativaAlunoAtivoSemestreRel.mrt"));

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("Conexão", "Conexão", Funcoes.GetArquivoConexaoBanco(), false));
                    report.Compile();

                    int idCampus = Convert.ToInt32(Request.QueryString["idCampus"]);
                    int idPeriodoLetivo = Convert.ToInt32(Request.QueryString["idPeriodoLetivo"]);
                    int calouro = Convert.ToInt32(Request.QueryString["calouro"]);

                    report.CompiledReport.DataSources["FonteDeDados1"].Parameters["pIdCampus"].ParameterValue = idCampus;
                    report.CompiledReport.DataSources["FonteDeDados1"].Parameters["pIdPeriodoLetivo"].ParameterValue = idPeriodoLetivo;
                    report.CompiledReport.DataSources["FonteDeDados1"].Parameters["pCalouro"].ParameterValue = calouro;

                    if (calouro == 1)
                    {
                        report["vMatriculaRematricula"] = "Calouro";
                    }
                    else if (calouro == 0)
                    {
                        report["vMatriculaRematricula"] = "Veterano";
                    }
                    else
                    {
                        report["vMatriculaRematricula"] = "Calouro / Veterano";
                    }

                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();

                    stiGestaoAdministrativaAlunoAtivoSemestreRel.Report = report;
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