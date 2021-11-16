using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;

namespace Sistema.Web.UI.Relatorio.View.Report.GestaoAdministrativa.Aspx
{
    public partial class GestaoAdministrativaEvasaoAreaCursoRel : CommonPage
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

                if (Audit.Autenticar("RF009"))
                {
                    report.Load(Server.MapPath("~/View/Report/GestaoAdministrativa/Stimull/GestaoAdministrativaEvasaoAreaCursoRel.mrt"));

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("SQL Server", "SQL Server", Funcoes.GetArquivoConexaoBanco(), false));
                    report.Compile();

                    int idCampus = Convert.ToInt32(Request.QueryString["idCampus"]);
                    int idPeriodoLetivo = Convert.ToInt32(Request.QueryString["idPeriodoLetivo"]);
                    string nomeCampus = Convert.ToString(Request.QueryString["nomeCampus"]);
                    string periodoBase = Convert.ToString(Request.QueryString["periodoBase"]);

                    report.CompiledReport.DataSources["SQLArea"].Parameters["IdPeriodoLetivo"].ParameterValue = idPeriodoLetivo;
                    report.CompiledReport.DataSources["SQLCurso"].Parameters["IdPeriodoLetivo"].ParameterValue = idPeriodoLetivo;
                    report.CompiledReport.DataSources["SQLUnivag"].Parameters["IdPeriodoLetivo"].ParameterValue = idPeriodoLetivo;


                    //DESCOMENTAR DEPOIS
                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();

                    report["vMatriculaRematricula"] = nomeCampus;
                    report["vPeriodoBase"] = periodoBase;

                    stiGestaoAdministrativaEvasaoAreaCurso.Report = report;

                    // StiReportResponse.ResponseAsPdf(report, false);

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