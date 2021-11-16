using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Web.UI.Relatorio.Util;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;

namespace Sistema.Web.UI.Relatorio.View.Report.GestaoAdministrativa.Aspx
{
    public partial class GestaoAdministrativaQuantitativoAlunoPercentualBolsaConvenioRel : CommonPage
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

                if (Audit.Autenticar("RF006"))
                {
                    report.Load(Server.MapPath("~/View/Report/GestaoAdministrativa/Stimull/GestaoAdministrativaQuantitativoAlunoPercentualBolsaConvenioRel.mrt"));

                    StiDataSource strD = new StiBusinessObjectSource();

                    report.Dictionary.Databases.Clear();
                    report.Dictionary.Databases.Add(new StiSqlDatabase("QtdAluno", "QtdAluno", Funcoes.GetArquivoConexaoBanco(), false));
                    report.Compile();

                    int idCampus = Convert.ToInt32(Request.QueryString["idCampus"]);
                    int idPeriodoLetivo = Convert.ToInt32(Request.QueryString["idPeriodoLetivo"]);

                    report.CompiledReport.DataSources["QtdAluno"].Parameters["pIdCampus"].ParameterValue = idCampus;
                    report.CompiledReport.DataSources["QtdAluno"].Parameters["pIdPeriodoLetivo"].ParameterValue = idPeriodoLetivo;

                    report["vUsuarioImpressao"] = Audit.RegisterRelatorioLog();


                    stiGestaoAdministrativaQuantitativoAlunoPercentualBolsaConvenioRel.Report = report;
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